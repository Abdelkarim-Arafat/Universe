using Universe.Application.EnrollmentServices.Dtos;
using Universe.Core.Enums;

namespace Universe.Application.EnrollmentServices.Commands.Update;

public class UpdateEnrollmentCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateEnrollmentCommand, Result<List<EnrollmentInfo>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<List<EnrollmentInfo>>> Handle(UpdateEnrollmentCommand command, CancellationToken ct)
    {
        // =========================
        // ✅ Step 0: Validate
        // =========================

        var isUserExists = await _unitOfWork.UserRepository
            .UserIsExistAsync(command.StudentId, ct);

        if (!isUserExists)
            return Result.Failure<List<EnrollmentInfo>>(StudentErrors.UserNotFound);

        var set = new HashSet<(Guid CourseOfferingId, SessionType Type)>();

        foreach (var item in command.NewEnrollments)
        {
            if (!set.Add((item.CourseOfferingId, item.Type)))
                return Result.Failure<List<EnrollmentInfo>>(EnrollmentErrors.DublicatedSessionWithSameType);
        }

        if (HasOverlapPerDay(command.NewEnrollments))
            return Result.Failure<List<EnrollmentInfo>>(EnrollmentErrors.DublicatedSessions);

        // =========================
        // 🧠 Step 1: Group Incoming
        // =========================

        var incomingSessionsGrouped = command.NewEnrollments
            .GroupBy(x => x.CourseOfferingId)
            .ToDictionary(g => g.Key, g => g.ToList());

        var incomingCourseOfferingIds = incomingSessionsGrouped.Keys.ToHashSet();

        var sessionIds = command.NewEnrollments
            .Select(x => x.SessionId)
            .ToList();

        // =========================
        // 🔥 Step 2: Get Existing (من الريبو)
        // =========================

        // 🧠 مطلوب:
        // يرجع كل enrollments + sessions بتاعتهم
        var existingEnrollments = await _unitOfWork.EnrollmentRepository
            .GetStudentEnrollmentsWithSessions(command.StudentId, ct);
        // لازم يرجع:
        // List<Enrollment> + Navigation: TeachingSessionEnrollments

        var existingCourseOfferingIds = existingEnrollments
            .Select(x => x.CourseOfferingId)
            .ToHashSet();

        // =========================
        // 🔥 Step 3: Diff Courses
        // =========================

        var toAddCourses = incomingCourseOfferingIds.Except(existingCourseOfferingIds).ToList();
        var toRemoveCourses = existingCourseOfferingIds.Except(incomingCourseOfferingIds).ToList();
        var toKeepCourses = existingCourseOfferingIds.Intersect(incomingCourseOfferingIds).ToList();

        // =========================
        // 🔥 Step 4: Bulk Data
        // =========================

        // 🧠 Session data (GroupNumber + Capacity)
        var sessionsData = await _unitOfWork.SessionRepository
                .GetGroupNumberAndCapacityBulkAsync(sessionIds, ct);

        // 🧠 seats
        var occupiedSeats = await _unitOfWork.EnrollmentRepository
            .GetOccupiedSeatsBulkAsync(sessionIds, ct);

        // 🧠 assessments
        var Assessments = await _unitOfWork.CourseOfferingRepository
            .GetCourseOfferingsAssessmentsBulkAsync(toAddCourses, ct);




        // =========================
        // 🔥 Step 5: Transaction
        // =========================

        using var trx = await _unitOfWork
            .Repository<Enrollment>()
            .BeginTransactionIsolatedAsync(ct);

        try
        {
            var enrollmentsToAdd = new List<Enrollment>();
            var enrollmentsToDelete = new List<Enrollment>();

            var sessionEnrollmentsToAdd = new List<TeachingSessionEnrollment>();
            var sessionEnrollmentsToDelete = new List<TeachingSessionEnrollment>();

            var AssessmentsToAdd = new List<StudentAssessment>();
            var AssessmentsToRemove = await _unitOfWork.UserRepository
            .GetStudentAssessmentByCourseOfferingBulkAsync(toRemoveCourses, command.StudentId, ct);


            // =========================
            // ➕ Add Courses
            // =========================

            foreach (var courseOfferingId in toAddCourses)
            {
                if (!incomingSessionsGrouped.TryGetValue(courseOfferingId, out var sessions))
                {
                    return Result.Failure<List<EnrollmentInfo>>(
                        CourseOfferingErrors.NotFound);
                    // check the error in future
                }

                var groupNumbers = sessions
                     .Select(s => sessionsData[s.SessionId].GroupNumber)
                     .Distinct();

                if (groupNumbers.Count() > 1)
                {
                    return Result.Failure<List<EnrollmentInfo>>(
                        EnrollmentErrors.DublicatedGroup);
                    
                }

                var first = sessions.First();

                var enrollment = new Enrollment
                {
                    StudentId = command.StudentId,
                    CourseId = first.CourseId,
                    CourseOfferingId = courseOfferingId,
                    GroupNumber = sessionsData[first.SessionId].GroupNumber,
                    Status = EnrollmentStatus.InProgress
                };

                enrollmentsToAdd.Add(enrollment);

                foreach (var s in sessions)
                {
                    if (!sessionsData.TryGetValue(s.SessionId, out var data))
                    {
                        return Result.Failure<List<EnrollmentInfo>>(
                            SessionErrors.NotFound);
                    }

                    if (!occupiedSeats.TryGetValue(s.SessionId, out var count))
                    {
                        return Result.Failure<List<EnrollmentInfo>>(
                            SessionErrors.NotFound);
                    }

                    if (count >= data.Capacity)
                        return Result.Failure<List<EnrollmentInfo>>(SessionErrors.NoAvailableSeats);

                    sessionEnrollmentsToAdd.Add(new TeachingSessionEnrollment
                    {
                        EnrollmentId = enrollment.Id,
                        TeachingSessionId = s.SessionId
                    });
                    occupiedSeats[s.SessionId]++;
                }
                if (!Assessments.TryGetValue(courseOfferingId, out var assessments))
                {
                    return Result.Failure<List<EnrollmentInfo>>(
                        CourseOfferingErrors.NotFound);
                    // add assessments errors
                }


                foreach (var ass in assessments)
                {
                    AssessmentsToAdd.Add(new StudentAssessment
                    {
                        StudentId = command.StudentId,
                        CourseOfferingAssessmentId = ass.Id,
                        CourseOfferingId = courseOfferingId,
                        degree = 0
                    });
                }
            }

            // =========================
            // ➖ Remove Courses
            // =========================

            foreach (var enrollment in existingEnrollments
                .Where(x => toRemoveCourses.Contains(x.CourseOfferingId)))
            {
                foreach (var session in enrollment.TeachingSessionEnrollments.Where(x => !x.IsDeleted))
                {
                    sessionEnrollmentsToDelete.Add(session);

                    if (occupiedSeats.TryGetValue(session.TeachingSessionId, out var count))
                    {
                        occupiedSeats[session.TeachingSessionId] = Math.Max(0, count - 1);
                    }
                }
                enrollmentsToDelete.Add(enrollment);
            }

            // =========================
            // 🔁 Update (Keep Courses)
            // =========================

            foreach (var enrollment in existingEnrollments
                     .Where(x => toKeepCourses.Contains(x.CourseOfferingId)))
            {
                var existingSessions = enrollment.TeachingSessionEnrollments
                    .Where(x => !x.IsDeleted)
                    .Select(x => x.TeachingSessionId)
                    .ToHashSet();

                if (!incomingSessionsGrouped.TryGetValue(enrollment.CourseOfferingId, out var sessions))
                {
                    return Result.Failure<List<EnrollmentInfo>>(CourseOfferingErrors.NotFound);
                    // add invalid data error
                }

                var incomingSessions = sessions
                    .Select(x => x.SessionId)
                    .ToHashSet();
                var groupNumbers = sessions
                     .Select(s => sessionsData[s.SessionId].GroupNumber)
                     .Distinct()
                      .ToList();

                if (groupNumbers.Count > 1)
                {
                    return Result.Failure<List<EnrollmentInfo>>(
                        EnrollmentErrors.DublicatedGroup);
                     
                }

                // ➕ Add new sessions

                foreach (var sId in incomingSessions.Except(existingSessions))
                {
                    if (!sessionsData.TryGetValue(sId, out var data) ||
                        !occupiedSeats.TryGetValue(sId, out var count))
                    {
                        return Result.Failure<List<EnrollmentInfo>>(SessionErrors.NotFound);
                    }

                    if (count >= data.Capacity)
                    {
                        return Result.Failure<List<EnrollmentInfo>>(SessionErrors.NoAvailableSeats);
                    }

                    sessionEnrollmentsToAdd.Add(new TeachingSessionEnrollment
                    {
                        EnrollmentId = enrollment.Id,
                        TeachingSessionId = sId
                    });
                    occupiedSeats[sId]++;
                }
                var sessionsToRemove = existingSessions.Except(incomingSessions).ToHashSet();
                // ➖ Remove sessions
                foreach (var tse in enrollment.TeachingSessionEnrollments
                    .Where(x => !x.IsDeleted && sessionsToRemove.Contains(x.TeachingSessionId)))
                {
                    sessionEnrollmentsToDelete.Add(tse);
                    if (occupiedSeats.TryGetValue(tse.TeachingSessionId, out var count))
                    {
                        occupiedSeats[tse.TeachingSessionId] = Math.Max(0, count - 1);
                    }
                }
            }
            // =========================
            // 💾 Save
            // =========================

            await _unitOfWork.Repository<Enrollment>()
                .AddRangeAsync(enrollmentsToAdd, ct);

            await _unitOfWork.Repository<TeachingSessionEnrollment>()
                .AddRangeAsync(sessionEnrollmentsToAdd, ct);

            await _unitOfWork.Repository<StudentAssessment>()
                .AddRangeAsync(AssessmentsToAdd, ct);

            _unitOfWork.Repository<Enrollment>()
               .DeletePermanentlyRange(enrollmentsToDelete);

            _unitOfWork.Repository<TeachingSessionEnrollment>()
               .DeletePermanentlyRange(sessionEnrollmentsToDelete);

            _unitOfWork.Repository<StudentAssessment>()
               .DeletePermanentlyRange(AssessmentsToRemove);


            await _unitOfWork.CompleteAsync(ct);
            await trx.CommitAsync(ct);


            var teachingSessionEnrollments = await _unitOfWork.EnrollmentRepository
           .GetTeachingSessionEnrollmentAsync(command.StudentId, ct);

            var response = teachingSessionEnrollments.Select(x => new EnrollmentInfo
            (
                x.EnrollmentId,
                x.TeachingSessionId,
                x.Enrollment.CourseOfferingId,
                x.Enrollment.CourseId,
                x.TeachingSession.Type,
                x.TeachingSession.StartTime,
                x.TeachingSession.EndTime,
                x.TeachingSession.Day
            )).ToList();

            return Result.Success(response);
        }
        catch (Exception ex)
        {
            await trx.RollbackAsync(ct);
            return Result.Failure<List<EnrollmentInfo>>(
                new Error("500", ex.Message, StatusCodes.Status409Conflict));
        }
    }
    private bool HasOverlapPerDay(IReadOnlyList<EnrollmentInfo> enrollmentInfos)
    {
        var groupedByDay = enrollmentInfos.GroupBy(e => e.DayOfWeek);
        foreach (var group in groupedByDay)
        {
            var sessions = group.OrderBy(e => e.StartTime).ToList();
            for (int i = 0; i < sessions.Count - 1; i++)
            {
                if (sessions[i].EndTime > sessions[i + 1].StartTime)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
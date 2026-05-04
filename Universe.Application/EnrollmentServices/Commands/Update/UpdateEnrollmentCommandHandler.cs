using Universe.Application.EnrollmentServices.Dtos;
using Universe.Core.Enums;

namespace Universe.Application.EnrollmentServices.Commands.Update;
 
public class UpdateEnrollmentCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateEnrollmentCommand, Result<List<EnrollmentInfo>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<List<EnrollmentInfo>>> Handle(UpdateEnrollmentCommand command, CancellationToken cancellationToken)
    {
        // فاليديشنز

        var isUserExists = await _unitOfWork.UserRepository
            .UserIsExistAsync(command.StudentId, cancellationToken);

        if (!isUserExists)
            return Result.Failure<List<EnrollmentInfo>>(StudentErrors.UserNotFound);

        var set = new HashSet<(Guid CourseOfferingId, SessionType Type)>();
        var sessionsToFunction = command.newSessions.Select(x => (x.SessionId, x.CourseOfferingId)).ToList();

        var newSessions = await _unitOfWork.SessionRepository
            .GetSessionsWithCourseOfferingIdAsync(sessionsToFunction, cancellationToken);

        foreach (var item in newSessions)
            if (!set.Add((item.CourseOfferingId, item.TeachingSession.Type)))
                return Result.Failure<List<EnrollmentInfo>>(EnrollmentErrors.DublicatedSessionWithSameType);
        
        if (HasOverlapPerDay(newSessions))
            return Result.Failure<List<EnrollmentInfo>>(EnrollmentErrors.DublicatedSessions);

        var StudentLevel = await _unitOfWork.LevelRepository
            .GetStudentCurrentLevelAsync(command.StudentId, cancellationToken);

        if (StudentLevel is null)
            return Result.Failure<List<EnrollmentInfo>>(LevelErrors.StudentLevelNotFound);

        var IsSemesterExist = await _unitOfWork.AcademicYearRepository
            .IsExistSemesterAsync(command.SemesterId, cancellationToken);

        if (!IsSemesterExist)
             return Result.Failure<List<EnrollmentInfo>>(SemesterErrors.NotFound);

        var StudyLoad = await _unitOfWork.StudyLoadByLevelRepository
            .GetByLevelIdAndSemesterIdAsync(StudentLevel.Id, command.SemesterId, cancellationToken);

        if (StudyLoad is null)
            return Result.Failure<List<EnrollmentInfo>>(StudyLoadRuleErrors.NotFound);

        var courseOfferingsIds = command.newSessions.Select(e => e.CourseOfferingId).ToList();

        decimal registredHours = await _unitOfWork.CourseOfferingRepository
            .RegistredHours(courseOfferingsIds, cancellationToken);

        if(registredHours < StudyLoad.MinHours || registredHours > StudyLoad.MaxHours)
            return Result.Failure<List<EnrollmentInfo>>(StudyLoadRuleErrors.NotAllowedHours);

        //  جمع البيانات الداخله


        var incomingSessionsGrouped = command.newSessions
            .GroupBy(x => x.CourseOfferingId)
            .ToDictionary(g => g.Key, g => g.ToList());

        var incomingCourseOfferingIds = incomingSessionsGrouped.Keys.ToHashSet();

        var sessionIds = command.newSessions
            .Select(x => x.SessionId)
            .ToList();


        // يرجع كل enrollments + sessions بتاعتهم
        var existingEnrollments = await _unitOfWork.EnrollmentRepository
            .GetStudentEnrollmentsWithSessions(command.StudentId, cancellationToken);

        //List<Enrollment> + TeachingSessionEnrollments

        var existingCourseOfferingIds = existingEnrollments
            .Select(x => x.CourseOfferingId)
            .ToHashSet();

   
        //Diff Courses
     

        var toAddCourses = incomingCourseOfferingIds.Except(existingCourseOfferingIds).ToList();
        var toRemoveCourses = existingCourseOfferingIds.Except(incomingCourseOfferingIds).ToList();
        var toKeepCourses = existingCourseOfferingIds.Intersect(incomingCourseOfferingIds).ToList();

        
        //(GroupNumber + Capacity)
        var sessionsData = await _unitOfWork.SessionRepository
                .GetGroupNumberAndCapacityBulkAsync(sessionIds, cancellationToken);

        var occupiedSeats = await _unitOfWork.EnrollmentRepository
            .GetOccupiedSeatsBulkAsync(sessionIds, cancellationToken);

        var Assessments = await _unitOfWork.CourseOfferingRepository
            .GetCourseOfferingsAssessmentsBulkAsync(toAddCourses, cancellationToken);


       
            var enrollmentsToAdd = new List<Enrollment>();
            var enrollmentsToDelete = new List<Enrollment>();

            var sessionEnrollmentsToAdd = new List<TeachingSessionEnrollment>();
            var sessionEnrollmentsToDelete = new List<TeachingSessionEnrollment>();

            var AssessmentsToAdd = new List<StudentAssessment>();
            var AssessmentsToRemove = await _unitOfWork.UserRepository
            .GetStudentAssessmentByCourseOfferingBulkAsync(toRemoveCourses, command.StudentId, cancellationToken);

            //  Add Courses

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

                var first = sessions.FirstOrDefault();

                if (first == null)
                    return Result.Failure<List<EnrollmentInfo>>(SessionErrors.NotFound);
                
                var enrollment = new Enrollment
                {
                    StudentId = command.StudentId,
                    CourseOfferingId = courseOfferingId,
                    GroupNumber = sessionsData[first.SessionId].GroupNumber,
                    Status = EnrollmentStatus.InProgress
                };

                enrollmentsToAdd.Add(enrollment);

                foreach (var session in sessions)
                {
                    if (!sessionsData.TryGetValue(session.SessionId, out var sessionData))
                        return Result.Failure<List<EnrollmentInfo>>(SessionErrors.NotFound);

                    if (!occupiedSeats.TryGetValue(session.SessionId, out var sessionCount))
                        return Result.Failure<List<EnrollmentInfo>>(SessionErrors.NotFound);

                    if (sessionCount >= sessionData.Capacity)
                        return Result.Failure<List<EnrollmentInfo>>(SessionErrors.NoAvailableSeats);

                    sessionEnrollmentsToAdd.Add(new TeachingSessionEnrollment
                    {
                        EnrollmentId = enrollment.Id,
                        TeachingSessionId = session.SessionId
                    });
                    occupiedSeats[session.SessionId]++;
                }

                if (!Assessments.TryGetValue(courseOfferingId, out var assessments))
                    return Result.Failure<List<EnrollmentInfo>>(CourseOfferingErrors.NotFoundAssessment);

                foreach (var ass in assessments)
                {
                    AssessmentsToAdd.Add(new StudentAssessment
                    {
                        StudentId = command.StudentId,
                        CourseOfferingAssessmentId = ass.Id
                    });
                }
            }

            //   Remove Courses

            foreach (var enrollment in existingEnrollments.Where(x => toRemoveCourses.Contains(x.CourseOfferingId)))
            {
                foreach (var session in enrollment.TeachingSessionEnrollments.Where(x => !x.IsDeleted))
                {
                    sessionEnrollmentsToDelete.Add(session);

                    if (occupiedSeats.TryGetValue(session.TeachingSessionId, out var sessionCount))
                        occupiedSeats[session.TeachingSessionId] = Math.Max(0, sessionCount - 1);
                }
                enrollmentsToDelete.Add(enrollment);
            }

            // Update Keep Courses

            foreach (var enrollment in existingEnrollments.Where(x => toKeepCourses.Contains(x.CourseOfferingId)))
            {
                var existingSessions = enrollment.TeachingSessionEnrollments
                    .Where(x => !x.IsDeleted)
                    .Select(x => x.TeachingSessionId)
                    .ToHashSet();

                if (!incomingSessionsGrouped.TryGetValue(enrollment.CourseOfferingId, out var sessions))
                    return Result.Failure<List<EnrollmentInfo>>(CourseOfferingErrors.NotFound);
                // add invalid data error

                var incomingSessions = sessions
                    .Select(x => x.SessionId)
                    .ToHashSet();

                var groupNumbers = sessions
                     .Select(s => sessionsData[s.SessionId].GroupNumber)
                     .Distinct()
                     .ToList();

                if (groupNumbers.Count > 1)
                    return Result.Failure<List<EnrollmentInfo>>(EnrollmentErrors.DublicatedGroup);

                // Add new sessions

                foreach (var sessionId in incomingSessions.Except(existingSessions))
                {
                    if (!sessionsData.TryGetValue(sessionId, out var sessionData) ||
                        !occupiedSeats.TryGetValue(sessionId, out var sessionsCount))
                        return Result.Failure<List<EnrollmentInfo>>(SessionErrors.NotFound);

                    if (sessionsCount >= sessionData.Capacity)
                        return Result.Failure<List<EnrollmentInfo>>(SessionErrors.NoAvailableSeats);

                    sessionEnrollmentsToAdd.Add(new TeachingSessionEnrollment
                    {
                        EnrollmentId = enrollment.Id,
                        TeachingSessionId = sessionId
                    });

                    occupiedSeats[sessionId]++;
                }

                var sessionsToRemove = existingSessions.Except(incomingSessions).ToHashSet();

                // Remove sessions

                foreach (var tse in enrollment.TeachingSessionEnrollments
                    .Where(x => !x.IsDeleted && sessionsToRemove.Contains(x.TeachingSessionId)))
                {
                    sessionEnrollmentsToDelete.Add(tse);
                    if (occupiedSeats.TryGetValue(tse.TeachingSessionId, out var sessionCount))
                        occupiedSeats[tse.TeachingSessionId] = Math.Max(0, sessionCount - 1);
                }
            }

        using var trx = await _unitOfWork
       .Repository<Enrollment>()
       .BeginTransactionIsolatedAsync(cancellationToken);

        try
        {
            if (sessionEnrollmentsToDelete.Any())
                _unitOfWork.Repository<TeachingSessionEnrollment>()
                   .DeletePermanentlyRange(sessionEnrollmentsToDelete);

            if (enrollmentsToDelete.Any())
                _unitOfWork.Repository<Enrollment>()
               .DeletePermanentlyRange(enrollmentsToDelete);

            if (AssessmentsToRemove.Any())
                _unitOfWork.Repository<StudentAssessment>()
               .DeletePermanentlyRange(AssessmentsToRemove);

            await _unitOfWork.Repository<Enrollment>()
                .AddRangeAsync(enrollmentsToAdd, cancellationToken);

            await _unitOfWork.Repository<TeachingSessionEnrollment>()
                .AddRangeAsync(sessionEnrollmentsToAdd, cancellationToken);

            await _unitOfWork.Repository<StudentAssessment>()
                .AddRangeAsync(AssessmentsToAdd, cancellationToken);

            await _unitOfWork.CompleteAsync(cancellationToken);
            await trx.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await trx.RollbackAsync(cancellationToken);
            return Result.Failure<List<EnrollmentInfo>> (
                new Error("500", ex.InnerException?.Message ?? ex.Message, StatusCodes.Status409Conflict));
        }

        var teachingSessionEnrollments = await _unitOfWork.EnrollmentRepository
          .GetTeachingSessionEnrollmentAsync(command.StudentId, cancellationToken);

        var response = teachingSessionEnrollments.Select(x => new EnrollmentInfo
        (
            x.EnrollmentId,
            x.TeachingSessionId,
            x.Enrollment.CourseOfferingId,
            x.TeachingSession.Type,
            x.TeachingSession.StartTime,
            x.TeachingSession.EndTime,
            x.TeachingSession.Day
        )).ToList();

        return Result.Success(response);
    }
    private bool HasOverlapPerDay(IReadOnlyList<CourseOfferingSession> enrollmentInfos)
    {
        var groupedByDay = enrollmentInfos.GroupBy(e => e.TeachingSession.Day);
        foreach (var group in groupedByDay)
        {
            var sessions = group.OrderBy(e => e.TeachingSession.StartTime).ToList();
            for (int i = 0; i < sessions.Count - 1; i++)
                if (sessions[i].TeachingSession.EndTime > sessions[i + 1].TeachingSession.StartTime)
                    return true;

        }
        return false;
    }
}
using Universe.Core.Contracts.Enrollments;
using Universe.Core.Enums;

namespace Universe.Application.EnrollmentServices.Commands.Update;
public class UpdateEnrollmentCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<UpdateEnrollmentCommand, Result<List<StudentExistingEnrollment>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<List<StudentExistingEnrollment>>>
        Handle(UpdateEnrollmentCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await ValidateUpdateEnrollmentAsync(command, cancellationToken);

        if (validationResult.IsFailure)
            return Result.Failure<List<StudentExistingEnrollment>>(validationResult.Error);

        var incommingSessionsDetails = validationResult.Value.sessionDetails;

        var sessionDetailsDict = validationResult.Value.sessionDetails
                                            .ToDictionary(s => s.sessionId);

        var incomingCourseOfferingIds = incommingSessionsDetails
                                            .Select(x => x.courseOfferingId)
                                            .ToHashSet();

        var executionData = await _unitOfWork.EnrollmentRepository
                         .GetEnrollmentExecutionDataAsync
                         (command.StudentId, command.SemesterId, incomingCourseOfferingIds, cancellationToken);

        var existingEnrollments = executionData.ExistingEnrollments;

        var incomingAssessmentsLookup = executionData.IncomingAssessmentsLookup;

        var incomingSessionsGrouped = incommingSessionsDetails
                                            .GroupBy(x => x.courseOfferingId)
                                            .ToDictionary(g => g.Key, g => g.ToList());

        var existingCourseOfferingIds = existingEnrollments
                                            .Select(x => x.CourseOfferingId)
                                            .ToHashSet();
        //Diff Courses
        var toAddCourses = incomingCourseOfferingIds.Except(existingCourseOfferingIds).ToList();
        var toRemoveCourses = existingCourseOfferingIds.Except(incomingCourseOfferingIds).ToList();
        var toKeepCourses = existingCourseOfferingIds.Intersect(incomingCourseOfferingIds).ToList();

        var enrollmentsToAdd = new List<Enrollment>();
        var enrollmentsToDelete = new List<Enrollment>();

        var sessionEnrollmentsToAdd = new List<TeachingSessionEnrollment>();
        var sessionEnrollmentsToDelete = new List<TeachingSessionEnrollment>();

        var AssessmentsToAdd = new List<StudentAssessment>();
        var AssessmentsToRemove = new List<StudentAssessment>();

        if (toRemoveCourses.Any())
            AssessmentsToRemove = await _unitOfWork.UserRepository
                .GetStudentAssessmentByCourseOfferingBulkAsync(toRemoveCourses, command.StudentId, cancellationToken);

        //  Add Courses
        foreach (var courseOfferingId in toAddCourses)
        {
            var courseSessions = incomingSessionsGrouped[courseOfferingId];

            if (!courseSessions.Any())
                continue;

            var firstSession = courseSessions.First();

            var enrollment = new Enrollment
            {
                StudentId = command.StudentId,
                CourseOfferingId = courseOfferingId,
                GroupNumber = firstSession.groupNumber,
                Status = EnrollmentStatus.InProgress
            };

            enrollmentsToAdd.Add(enrollment);

            foreach (var session in courseSessions)
            {

                if (session.occupiedSeats >= session.capacity)
                    return Result.Failure<List<StudentExistingEnrollment>>(SessionErrors.NoAvailableSeats);

                sessionEnrollmentsToAdd.Add(new TeachingSessionEnrollment
                {
                    EnrollmentId = enrollment.Id,
                    TeachingSessionId = session.sessionId
                });
            }

            var assessmentsIds = incomingAssessmentsLookup[courseOfferingId];

            if (!assessmentsIds.Any())
                return Result.Failure<List<StudentExistingEnrollment>>(CourseOfferingErrors.NotFoundAssessment);

            foreach (var assessmentId in assessmentsIds)
            {
                AssessmentsToAdd.Add(new StudentAssessment
                {
                    StudentId = command.StudentId,
                    CourseOfferingAssessmentId = assessmentId
                });
            }
        }

        //   Remove Courses
        foreach (var enrollment in existingEnrollments.Where(x => toRemoveCourses.Contains(x.CourseOfferingId)))
        {
            foreach (var session in enrollment.TeachingSessionEnrollments)
                sessionEnrollmentsToDelete.Add(session);

            enrollmentsToDelete.Add(enrollment);
        }

        // Update Keep Courses
        foreach (var enrollment in executionData.ExistingEnrollments.Where(x => toKeepCourses.Contains(x.CourseOfferingId)))
        {

            var existingSessions = enrollment.TeachingSessionEnrollments
                .Select(x => x.TeachingSessionId)
                .ToHashSet();

            if (!incomingSessionsGrouped.TryGetValue(enrollment.CourseOfferingId, out var newSessionsList))
                return Result.Failure<List<StudentExistingEnrollment>>(CourseOfferingErrors.NotFound);

            var incomingSessionIds = newSessionsList.Select(x => x.sessionId).ToHashSet();

            var sessionsToAdd = incomingSessionIds.Except(existingSessions).ToList();
            var sessionsToRemove = existingSessions.Except(incomingSessionIds).ToHashSet();

            foreach (var sessionId in sessionsToAdd)
            {
                if (!sessionDetailsDict.TryGetValue(sessionId, out var sessionInfo))
                    return Result.Failure<List<StudentExistingEnrollment>>(SessionErrors.NotFound);

                if (sessionInfo.occupiedSeats >= sessionInfo.capacity)
                    return Result.Failure<List<StudentExistingEnrollment>>(SessionErrors.NoAvailableSeats);

                sessionEnrollmentsToAdd.Add(new TeachingSessionEnrollment
                {
                    EnrollmentId = enrollment.Id,
                    TeachingSessionId = sessionId
                });
            }

            foreach (var tse in enrollment.TeachingSessionEnrollments
                .Where(x => sessionsToRemove.Contains(x.TeachingSessionId)))
                sessionEnrollmentsToDelete.Add(tse);

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
            return Result.Failure<List<StudentExistingEnrollment>>(
                new Error("500", ex.InnerException?.Message ?? ex.Message, StatusCodes.Status409Conflict));
        }

        var studentSchedule = await _unitOfWork.EnrollmentRepository
            .GetStudentScheduleAsync(command.StudentId, cancellationToken);

        return Result.Success(studentSchedule);
    }
    private async Task<Result<UpdateEnrollmentValidationDto>> ValidateUpdateEnrollmentAsync(
    UpdateEnrollmentCommand command,
    CancellationToken cancellationToken)
    {
        var courseOfferingsIds = command.newSessions.Select(e => e.CourseOfferingId).Distinct().ToList();
        var sessionIds = command.newSessions.Select(e => e.SessionId).Distinct().ToList();

        var validationResult = await _unitOfWork.EnrollmentRepository
            .GetUpdateEnrollmentValidationDataAsync
            (command.StudentId, command.SemesterId, courseOfferingsIds, sessionIds, cancellationToken);

        if (validationResult is null)
            return Result.Failure<UpdateEnrollmentValidationDto>(StudentErrors.UserNotFound);

        if (!validationResult.isSemesterExist)
            return Result.Failure<UpdateEnrollmentValidationDto>(SemesterErrors.NotFound);

        if (!validationResult.minHours.HasValue || !validationResult.maxHours.HasValue)
            return Result.Failure<UpdateEnrollmentValidationDto>(StudyLoadRuleErrors.NotFound);

        if (validationResult.registeredHours < validationResult.minHours
            || validationResult.registeredHours > validationResult.maxHours)
            return Result.Failure<UpdateEnrollmentValidationDto>(StudyLoadRuleErrors.NotAllowedHours);

        var typeCheckSet = new HashSet<(Guid CourseOfferingId, SessionType Type)>();
        var courseGroupMap = new Dictionary<Guid, int>();

        foreach (var item in validationResult.sessionDetails)
        {
            if (!typeCheckSet.Add((item.courseOfferingId, item.type)))
                return Result.Failure<UpdateEnrollmentValidationDto>(EnrollmentErrors.DublicatedSessionWithSameType);

            if (courseGroupMap.TryGetValue(item.courseOfferingId, out var currentGroup))
            {
                if (currentGroup != item.groupNumber)
                    return Result.Failure<UpdateEnrollmentValidationDto>(EnrollmentErrors.DublicatedGroup);
            }
            else
                courseGroupMap[item.courseOfferingId] = item.groupNumber;
        }

        if (HasOverlapPerDay(validationResult.sessionDetails))
            return Result.Failure<UpdateEnrollmentValidationDto>(EnrollmentErrors.DublicatedSessions);

        return Result.Success(validationResult);
    }
    private bool HasOverlapPerDay(List<SessionDetailsDto> sessionDetails)
    {
        var groupedByDay = sessionDetails.GroupBy(e => e.day);
        foreach (var group in groupedByDay)
        {
            var sessions = group.OrderBy(e => e.startTime).ToList();
            for (int i = 0; i < sessions.Count - 1; i++)
                if (sessions[i].endTime > sessions[i + 1].startTime)
                    return true;

        }
        return false;
    }
}
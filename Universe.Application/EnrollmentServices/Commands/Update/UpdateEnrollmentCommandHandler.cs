using Universe.Core.Enums;

namespace Universe.Application.EnrollmentServices.Commands.Update;
public class UpdateEnrollmentCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<UpdateEnrollmentCommand, Result<List<StudentExistingEnrollment>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<List<StudentExistingEnrollment>>>
        Handle(UpdateEnrollmentCommand command, CancellationToken cancellationToken)

    {
        var sessionIds = command.newSessions.Select(e => e.SessionId).Distinct().ToList();
        var incomingCourseOfferingIds = command.newSessions.Select(e => e.CourseOfferingId).Distinct().ToList();

        var incomingSessionsDetails = await _unitOfWork.SessionRepository
            .GetIncomingSessionsDetailsAsync(sessionIds, cancellationToken);

        var result = await ValidateBusinessRulesAsync
             (command, incomingSessionsDetails, incomingCourseOfferingIds, cancellationToken);

        if (result.IsFailure)
            return Result.Failure<List<StudentExistingEnrollment>>(result.Error);

        var sessionDetailsDict = incomingSessionsDetails.ToDictionary(s => s.sessionId);

        var existingEnrollments = await _unitOfWork.EnrollmentRepository
               .GetExistingEnrollmentIncludingSessionsAsync(command.StudentId, command.SemesterId, cancellationToken);

        var incomingSessionsGrouped = incomingSessionsDetails.GroupBy(x => x.courseOfferingId).ToDictionary(g => g.Key, g => g.ToList());

        var existingCourseOfferingIds = existingEnrollments
                                            .Select(x => x.CourseOfferingId).ToList();
        //Diff Courses
        var toAddCourses = incomingCourseOfferingIds.Except(existingCourseOfferingIds).ToList();
        var toRemoveCourses = existingCourseOfferingIds.Except(incomingCourseOfferingIds).ToList();
        var toKeepCourses = existingCourseOfferingIds.Intersect(incomingCourseOfferingIds).ToList();

        var enrollmentsToAdd = new List<Enrollment>();
        var enrollmentsToDelete = new List<Enrollment>();

        var enrollmentsSessionsToAdd = new List<TeachingSessionEnrollment>();
        var enrollmentsSessionsToDelete = new List<TeachingSessionEnrollment>();

        var assessmentsToAdd = new List<StudentAssessment>();
        var assessmentsToRemove = new List<StudentAssessment>();

        if (toRemoveCourses.Any())
            assessmentsToRemove = await _unitOfWork.StudentAssessmentRepository
                .GetStudentAssessmentByCourseOfferingBulkAsync(toRemoveCourses, command.StudentId, cancellationToken);

        var incomingAssessmentsLookup = await _unitOfWork.CourseOfferingRepository
       .GetAssessmentIdsGroupedByOfferingAsync(toAddCourses, cancellationToken);

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

                enrollmentsSessionsToAdd.Add(new TeachingSessionEnrollment
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
                assessmentsToAdd.Add(new StudentAssessment
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
                enrollmentsSessionsToDelete.Add(session);

            enrollmentsToDelete.Add(enrollment);
        }

        // Update Keep Courses
        foreach (var enrollment in existingEnrollments.Where(x => toKeepCourses.Contains(x.CourseOfferingId)))
        {

            var existingSessionsIds = enrollment.TeachingSessionEnrollments
                .Select(x => x.TeachingSessionId)
                .ToHashSet();

            if (!incomingSessionsGrouped.TryGetValue(enrollment.CourseOfferingId, out var newSessionsDetails))
                return Result.Failure<List<StudentExistingEnrollment>>(CourseOfferingErrors.NotFound);

            var incomingSessionIds = newSessionsDetails.Select(x => x.sessionId).ToHashSet();

            var sessionsToAdd = incomingSessionIds.Except(existingSessionsIds).ToList();
            var sessionsToRemove = existingSessionsIds.Except(incomingSessionIds).ToList();

            foreach (var sessionId in sessionsToAdd)
            {
                if (!sessionDetailsDict.TryGetValue(sessionId, out var sessionInfo))
                    return Result.Failure<List<StudentExistingEnrollment>>(SessionErrors.NotFound);

                if (sessionInfo.occupiedSeats >= sessionInfo.capacity)
                    return Result.Failure<List<StudentExistingEnrollment>>(SessionErrors.NoAvailableSeats);

                enrollmentsSessionsToAdd.Add(new TeachingSessionEnrollment
                {
                    EnrollmentId = enrollment.Id,
                    TeachingSessionId = sessionId
                });
            }

            foreach (var tse in enrollment.TeachingSessionEnrollments
                .Where(x => sessionsToRemove.Contains(x.TeachingSessionId)))
            {

                enrollmentsSessionsToDelete.Add(tse);
            }
        }

        using var trx = await _unitOfWork
       .BeginTransactionIsolatedAsync(cancellationToken);

        try
        {
            if (enrollmentsSessionsToDelete.Any())
                _unitOfWork.Repository<TeachingSessionEnrollment>()
                   .DeletePermanentlyRange(enrollmentsSessionsToDelete);

            if (enrollmentsToDelete.Any())
                _unitOfWork.Repository<Enrollment>()
               .DeletePermanentlyRange(enrollmentsToDelete);

            if (assessmentsToRemove.Any())
                _unitOfWork.Repository<StudentAssessment>()
               .DeletePermanentlyRange(assessmentsToRemove);

            await _unitOfWork.Repository<Enrollment>()
                .AddRangeAsync(enrollmentsToAdd, cancellationToken);

            await _unitOfWork.Repository<TeachingSessionEnrollment>()
                .AddRangeAsync(enrollmentsSessionsToAdd, cancellationToken);

            await _unitOfWork.Repository<StudentAssessment>()
                .AddRangeAsync(assessmentsToAdd, cancellationToken);

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
            .GetStudentScheduleAsync(command.StudentId,command.SemesterId, cancellationToken);

        return Result.Success(studentSchedule);
    }
    private async Task<Result> ValidateBusinessRulesAsync(
    UpdateEnrollmentCommand command,
    List<SessionDetailsDto> sessionDetails,
    List<Guid> courseOfferingsIds,
    CancellationToken cancellationToken)
    {
        var isUserExist = await _unitOfWork.UserRepository.IsUserExistAsync(command.StudentId, cancellationToken);

        if (!isUserExist)
            return Result.Failure(StudentErrors.UserNotFound);

        var semester = await _unitOfWork.AcademicYearRepository
            .GetSemesterByIdAsync(command.SemesterId, cancellationToken);

        if (semester == null)
            return Result.Failure(SemesterErrors.NotFound);

        var studentCurrentProgramId = await _unitOfWork.AcademicProgramRepository
            .GetStudentCurrentProgramIdAsync(command.StudentId, cancellationToken);

        if (!studentCurrentProgramId.HasValue)
            return Result.Failure(StudentErrors.NoProgram);

        var totalEarnedHours = await _unitOfWork.UserRepository
           .CalculateCreditHoursAsync(command.StudentId, null, cancellationToken);

        var studentCurrentLevelId = await _unitOfWork.LevelRepository
            .GetStudentCurrentLevelIdAsync
            (studentCurrentProgramId.Value, totalEarnedHours, cancellationToken);

        if (!studentCurrentLevelId.HasValue)
            return Result.Failure(LevelErrors.StudentLevelNotFound);

        var levelStudyLoad = await _unitOfWork.StudyLoadByLevelRepository
            .GetLevelStudyLoadAsync(studentCurrentLevelId.Value, semester.Name, cancellationToken);

        if (levelStudyLoad == null)
            return Result.Failure(StudyLoadByLevelErrors.NotFound);

        var incomingCreditHours = await _unitOfWork.CourseOfferingRepository
            .CalculateCreditHoursForCoursesAsync(courseOfferingsIds, cancellationToken);

        if (incomingCreditHours < levelStudyLoad.MinHours || incomingCreditHours > levelStudyLoad.MaxHours)
            return Result.Failure(StudyLoadRuleErrors.NotAllowedHours);

        var typeCheckSet = new HashSet<(Guid CourseOfferingId, SessionType Type)>();
        var courseGroupMap = new Dictionary<Guid, int>();

        foreach (var item in sessionDetails)
        {
            if (!typeCheckSet.Add((item.courseOfferingId, item.type)))
                return Result.Failure(EnrollmentErrors.DuplicatedSessionWithSameType);

            if (courseGroupMap.TryGetValue(item.courseOfferingId, out var currentGroup))
            {
                if (currentGroup != item.groupNumber)
                    return Result.Failure(EnrollmentErrors.DuplicatedGroup);
            }
            else
                courseGroupMap[item.courseOfferingId] = item.groupNumber;
        }

        if (HasOverlapPerDay(sessionDetails))
            return Result.Failure(EnrollmentErrors.DublicatedSessions);

        return Result.Success(sessionDetails);
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
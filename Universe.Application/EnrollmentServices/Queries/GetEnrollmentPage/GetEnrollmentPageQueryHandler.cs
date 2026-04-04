using Universe.Application.CourseServices.Dtos;
using Universe.Application.EnrollmentServices.Dtos;
using Universe.Application.LevelServices.LevelDtos;
using Universe.Application.TeachingSessionServices.SessionDtos;
using Universe.Application.UserServices.UserDtos;

namespace Universe.Application.EnrollmentServices.Queries.GetEnrollmentPage;

public class GetEnrollmentPageQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetEnrollmentPageQuery, Result<EnrollmentPageResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<EnrollmentPageResponse>> Handle(GetEnrollmentPageQuery query, CancellationToken cancellationToken)
    {
        var Student = await _unitOfWork.UserRepository
            .GetStudentByIdAsync(query.StudentId, cancellationToken);

        if (Student is null)
            return Result.Failure<EnrollmentPageResponse>(StudentErrors.UserNotFound);

        var StudentLevel = await _unitOfWork.UserRepository.GetCurrentLevelAsync(Student.Id, cancellationToken);

        if (StudentLevel is null)
            return Result.Failure<EnrollmentPageResponse>(LevelErrors.NotFound);

        decimal Gpa = await _unitOfWork.UserRepository.CalculateComulativeGpaAsync(Student.Id, cancellationToken);

        var StudyRules = await _unitOfWork.StudyLoadRuleRepository
            .GetByGpaAsync(Gpa, cancellationToken);
        if (StudyRules is null)
            return Result.Failure<EnrollmentPageResponse>(StudyLoadRuleErrors.NotFound);

        var isSemesterExist = await _unitOfWork.AcademicYearRepository
            .IsSemesterExistAsync(query.SemesterId, cancellationToken);
        if (!isSemesterExist)
            return Result.Failure<EnrollmentPageResponse>(SemesterErrors.NotFound);

        var AcademicProgramId = await _unitOfWork.UserRepository.GetCurrentProgram(query.StudentId, cancellationToken);

        if (AcademicProgramId is null)
            return Result.Failure<EnrollmentPageResponse>(StudentErrors.NoProgram);

        var Level = await _unitOfWork.LevelRepository
            .GetByIdAsync(query.LevelId, cancellationToken);
        if (Level is null)
            return Result.Failure<EnrollmentPageResponse>(LevelErrors.NotFound);


        var CourseOfferings = await _unitOfWork.CourseOfferingRepository
            .GetCourseOfferingsByLevelAndSemesterIncludingCourseAsync(query.LevelId, query.SemesterId, cancellationToken);


        var teachingSessionEnrollments = await _unitOfWork.EnrollmentRepository
    .GetTeachingSessionEnrollmentAsync(query.StudentId, cancellationToken);

        var EnrollmentInfos = teachingSessionEnrollments.Select(x => new EnrollmentInfo
           (
            x.EnrollmentId,
            x.TeachingSessionId,
            x.Enrollment.CourseOfferingId,
            x.Enrollment.CourseId,
            x.TeachingSession.Type,
            x.TeachingSession.StartTime,
            x.TeachingSession.EndTime,
            x.TeachingSession.Day)).ToList();

        var CourseOfferingsResponse = new List<CourseRegistrationResponse> { };

        foreach (var courseOffering in CourseOfferings)
        {

            var preRequisitesIds = await _unitOfWork.CourseRepository
                .GetDirectPreRequisitesIdsAsync(courseOffering.CourseId, cancellationToken);

            int Count = 0;
            foreach (var Id in preRequisitesIds)
            {
                bool PassedInPreRequisite = await _unitOfWork.EnrollmentRepository
                    .IsStudentPassedInCourse(query.StudentId, Id, cancellationToken);
                Count += PassedInPreRequisite ? 1 : 0;
            }

            if (Count != preRequisitesIds.Count())
                continue;

            var Sessions = await _unitOfWork.SessionRepository
                .GetSessionsByCourseOfferingIncludingInstructor(courseOffering.Id, cancellationToken);

            var SessionsOption = Sessions.Select(async session => new SessionOptionResponse(
                 session.Id,
                 session.Instructor.Name,
                 session.Type,
                 session.GroupNumber,
                 session.Day,
                 session.StartTime,
                 session.EndTime,
                 await _unitOfWork.EnrollmentRepository.AvailableSeatsInSession(session.Id, cancellationToken)));

            var CourseRegistration = new CourseRegistrationResponse
                (courseOffering.Id,
                courseOffering.CourseId,
                courseOffering.Course.Name,
                courseOffering.Course.Code,
                courseOffering.IsOptional,
                courseOffering.CreditHours,
                EnrollmentInfos.Any(x => x.CourseOfferingId == courseOffering.Id),
                (List<SessionOptionResponse>)SessionsOption
                );
            CourseOfferingsResponse.Add(CourseRegistration);
        }

        var LevelResponse = new LevelCoursesResponse
            (
             Level.Name,
             CourseOfferingsResponse
            );

        var StudentInfo = new StudentInfoResponse
           (
           Student.Name, StudentLevel.Name,
           Student.StudentCode, 0,
           StudyRules.MaxHours, StudyRules.MinHours, Gpa);

        var Response = new EnrollmentPageResponse(StudentInfo, LevelResponse, EnrollmentInfos);

        return Result.Success(Response);
    }
}

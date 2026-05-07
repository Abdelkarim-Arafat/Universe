using Universe.Application.EnrollmentServices.Dtos;
using Universe.Application.UserServices.UserDtos;
using Universe.Core.Contracts.Enrollments;

namespace Universe.Application.EnrollmentServices.Queries.GetEnrollmentPage;

public class GetEnrollmentPageQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetEnrollmentPageQuery, Result<EnrollmentPageResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<EnrollmentPageResponse>> Handle(GetEnrollmentPageQuery query, CancellationToken cancellationToken)
    {

        var enrollmentContextDto = await _unitOfWork.EnrollmentRepository
            .GetEnrollmentValidationContextAsync(query.StudentId, query.SemesterId, cancellationToken);

        if (enrollmentContextDto is null)
            return Result.Failure<EnrollmentPageResponse>(StudentErrors.UserNotFound);

        if (enrollmentContextDto.StudentLevelName is null)
            return Result.Failure<EnrollmentPageResponse>(LevelErrors.StudentLevelNotFound);

        if ((enrollmentContextDto.MinHours is null) || (enrollmentContextDto.MaxHours is null))
            return Result.Failure<EnrollmentPageResponse>(StudyLoadRuleErrors.NotFound);

        if (!enrollmentContextDto.IsSemesterValid)
            return Result.Failure<EnrollmentPageResponse>(SemesterErrors.NotFound);

        if (enrollmentContextDto.AcademicProgramId is null)
            return Result.Failure<EnrollmentPageResponse>(StudentErrors.NoProgram);

        var levelAvailableCourses = await _unitOfWork.CourseOfferingRepository
               .GetAvailableCoursesCatalogAsync
               (query.StudentId, query.SemesterId, query.LevelId, cancellationToken);

        if (levelAvailableCourses is null)
            return Result.Failure<EnrollmentPageResponse>(LevelErrors.NotFound);

        var updatedCourses = levelAvailableCourses.Courses.Select(course =>
        {
            var existingEnrollment = enrollmentContextDto.ExistingEnrollments
                .FirstOrDefault(e => e.CourseOfferingId == course.CourseOfferingId);

            if (existingEnrollment == null)
                return course;

            var updatedSessions = course.Sessions.Select(session =>
            {
                if (session.SessionId == existingEnrollment.SessionId)
                {
                    return session with { IsRegistered = true };
                }

                return session;
            }).ToList(); 

            return course with
            {
                IsEnrolled = true,
                Sessions = updatedSessions
            };

        }).ToList(); 

        var updatedLevelInfo = new LevelRegistrationCatalogDto( 
            levelAvailableCourses.LevelName,
            updatedCourses
        );

        decimal Gpa = await _unitOfWork.UserRepository
            .CalculateGpaAsync(query.StudentId, null, cancellationToken);

        var StudentInfo = new StudentInfoResponse
           (
            enrollmentContextDto.StudentName,
            enrollmentContextDto.StudentLevelName,
            enrollmentContextDto.StudentCode,
            enrollmentContextDto.CurrentRegisteredHours,
            enrollmentContextDto.MaxHours!.Value,
            enrollmentContextDto.MinHours!.Value,
            Gpa
           );

        var Response = new EnrollmentPageResponse(
            StudentInfo,
            updatedLevelInfo,
            enrollmentContextDto.ExistingEnrollments);

        return Result.Success(Response);
    }
}

using System.Security.Claims;
using Universe.Application.EnrollmentServices.Dtos;

namespace Universe.Application.UserServices.Querys.GetStudentSchedule;

internal class GetStudentScheduleQueryHandler(
    IUnitOfWork unitOfWork,
    IHttpContextAccessor httpContextAccessor
    ) : IRequestHandler<GetStudentScheduleQuery, Result<List<EnrollmentInfo>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result<List<EnrollmentInfo>>> Handle(GetStudentScheduleQuery request, CancellationToken cancellationToken)
    {
        var User = _httpContextAccessor.HttpContext?.User;
        var value = User?.FindFirstValue(ClaimTypes.NameIdentifier);
        var StudentId = Guid.TryParse(value, out var userId) ? userId : Guid.Empty;
        var IsStudentExist = await _unitOfWork.UserRepository.UserIsExistAsync(StudentId, cancellationToken);

        if (!IsStudentExist)
            return Result.Failure<List<EnrollmentInfo>>(StudentErrors.UserNotFound);

        var teachingSessionEnrollments = await _unitOfWork.EnrollmentRepository
   .GetTeachingSessionEnrollmentAsync(StudentId, cancellationToken);

        var response = teachingSessionEnrollments.Select(x => new EnrollmentInfo
           (
            x.EnrollmentId,
            x.TeachingSessionId,
            x.Enrollment.CourseOfferingId,
            x.TeachingSession.Type,
            x.TeachingSession.StartTime,
            x.TeachingSession.EndTime,
            x.TeachingSession.Day)).ToList();

        return Result.Success(response);
    }
}

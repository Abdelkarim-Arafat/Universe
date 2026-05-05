using System.Security.Claims;
using Universe.Core.Dtos.Enrollments;

namespace Universe.Application.UserServices.Querys.GetStudentSchedule;

internal class GetStudentScheduleQueryHandler(
    IUnitOfWork unitOfWork,
    IHttpContextAccessor httpContextAccessor
    ) : IRequestHandler<GetStudentScheduleQuery, Result<List<StudentExistingEnrollment>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result<List<StudentExistingEnrollment>>> Handle(GetStudentScheduleQuery request, CancellationToken cancellationToken)
    {
        var User = _httpContextAccessor.HttpContext?.User;
        var value = User?.FindFirstValue(ClaimTypes.NameIdentifier);
        var StudentId = Guid.TryParse(value, out var userId) ? userId : Guid.Empty;

        var studentSchedule = await _unitOfWork.EnrollmentRepository
            .GetStudentScheduleAsync(StudentId, cancellationToken);

        return Result.Success(studentSchedule);
    }
}

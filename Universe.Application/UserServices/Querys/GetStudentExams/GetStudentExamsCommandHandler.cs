using System.Security.Claims;
using Universe.Application.UserServices.UserDtos;
using Universe.Core.Dtos.Student;

namespace Universe.Application.UserServices.Querys.GetStudentExams;

internal class GetStudentExamsCommandHandler(
    IUnitOfWork unitOfWork,
    IHttpContextAccessor httpContextAccessor
    ) : IRequestHandler<GetStudentExamsCommand, Result<StudentExamsResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result<StudentExamsResponse>> Handle(GetStudentExamsCommand request, CancellationToken cancellationToken)
    {
        var User = _httpContextAccessor.HttpContext?.User;
        var value = User?.FindFirstValue(ClaimTypes.NameIdentifier);
        var StudentId = Guid.TryParse(value, out var userId) ? userId : Guid.Empty;
        var IsStudentExist = await _unitOfWork.UserRepository.UserIsExistAsync(StudentId, cancellationToken);

        if (!IsStudentExist)
            return Result.Failure<StudentExamsResponse>(StudentErrors.UserNotFound);

        var response = await _unitOfWork.UserRepository.GetStudentExamsTablesAsync(StudentId, cancellationToken);

        return Result.Success(response);
    }
}

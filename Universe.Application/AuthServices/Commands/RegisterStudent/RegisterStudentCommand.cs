
using Universe.Application.UserServices.UserDtos;

namespace Universe.Application.AuthServices.Commands.Register;

public record RegisterStudentCommand(
    [Required] Guid CollegeId,
    string Name,
    string StudentCode,
    string NationalId,
    string UserName,
    string Password
) : IRequest<Result<RegisterStudentResponse>>;

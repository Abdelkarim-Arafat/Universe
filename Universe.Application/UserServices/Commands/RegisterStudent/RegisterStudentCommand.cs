using Universe.Application.UserServices.UserDtos;
using Universe.Core.Enums;

namespace Universe.Application.UserServices.Commands.RegisterStudent;

public record RegisterStudentCommand(
    [Required] Guid CollegeId,
    string Name,
    string StudentCode,
    string NationalIdOrPassport,
    string UserName,
    string Password
) : IRequest<Result<RegisterStudentResponse>>;

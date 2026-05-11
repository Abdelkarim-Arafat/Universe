using Universe.Core.Contracts.User;

namespace Universe.Application.UserServices.Commands.RegisterStudent;

public record RegisterStudentCommand (
    [Required] Guid CollegeId,
    [Required] Guid ProgramId,
    string Name,
    string StudentCode,
    string NationalIdOrPassport,
    string UserName,
    string Password
) : IRequest<Result<RegisterStudentResponse>>;
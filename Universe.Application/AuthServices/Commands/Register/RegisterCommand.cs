
namespace Universe.Application.AuthServices.Commands.Register;

public record RegisterCommand(
    string Email,
    string Password,
    string Name
) : IRequest<Result>;

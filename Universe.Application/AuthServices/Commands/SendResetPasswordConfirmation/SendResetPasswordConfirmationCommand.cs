
namespace Universe.Application.AuthServices.Commands.SendResetPasswordCodeAsync;

public record SendResetPasswordConfirmationCommand(
    string UserName,
    string Email
) : IRequest<Result>;
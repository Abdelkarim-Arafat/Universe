
namespace Universe.Application.AuthServices.Commands.ResetPassword;

public record ResetPasswordCommand(
    string Email,
    string NewPassword
) : IRequest<Result>;
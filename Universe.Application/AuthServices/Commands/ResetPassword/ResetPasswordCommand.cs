
namespace Universe.Application.AuthServices.Commands.ResetPassword;

public record ResetPasswordCommand(
    string Email,
    string Code,
    string NewPassword
) : IRequest<Result>;
namespace Universe.Application.AuthServices.Commands.VerificationResetPasswordCode;

public record VerificationResetPasswordCodeCommand(
    string Email,
    string Code
) : IRequest<Result>;
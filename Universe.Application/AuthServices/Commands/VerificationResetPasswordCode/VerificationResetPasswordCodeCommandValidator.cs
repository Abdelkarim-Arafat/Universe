
using Universe.Application.AuthServices.Commands.VerificationResetPasswordCode;

namespace Universe.Application.AuthServices.Commands.VerificationResetPasswordCodeCommandValidator;

public class VerificationResetPasswordCodeCommandValidator : AbstractValidator<VerificationResetPasswordCodeCommand>
{
    public VerificationResetPasswordCodeCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress().WithMessage("email address is required.");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Verification code is required.");
    }
}

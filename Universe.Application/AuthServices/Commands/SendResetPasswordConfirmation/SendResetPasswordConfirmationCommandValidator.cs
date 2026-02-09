using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.AuthServices.Commands.SendResetPasswordCodeAsync;

namespace Universe.Application.AuthServices.Commands.SendResetPasswordConfirmation;

public class VerificationResetPasswordCodeCommandValidator : AbstractValidator<SendResetPasswordConfirmationCommand>
{
    public VerificationResetPasswordCodeCommandValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage("Username is required.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("email address is required.");
    }
}

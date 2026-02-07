using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.AuthServices.Commands.SendResetPasswordCodeAsync;

namespace Universe.Application.AuthServices.Commands.SendResetPasswordConfirmation;

public class SendResetPasswordConfirmationCommandValidator : AbstractValidator<SendResetPasswordConfirmationCommand>
{
    public SendResetPasswordConfirmationCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
    }
}

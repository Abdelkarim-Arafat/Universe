using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.AuthServices.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Username is required.");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.");
    }
}


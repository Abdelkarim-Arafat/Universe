using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.AuthServices.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8);

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);
    }
}
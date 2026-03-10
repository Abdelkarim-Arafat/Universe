using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.UserServices.Commands.UpdatePersonalData;

public class UpdatePersonalDataCommandValidator : AbstractValidator<UpdatePersonalDataCommand>
{
    public UpdatePersonalDataCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

        RuleFor(x => x.StudentCode)
            .NotEmpty().WithMessage("Student code is required.")
            .MaximumLength(20).WithMessage("Student code cannot exceed 20 characters.");

        RuleFor(x => x.NationalIdOrPassport)
            .NotEmpty().WithMessage("National ID or Passport is required.")
            .MaximumLength(50).WithMessage("National ID or Passport cannot exceed 50 characters.");

        RuleFor(x => x.Religion)
            .NotEmpty()
            .IsInEnum().WithMessage("Invalid religion value.");
        
        RuleFor(x => x.Gender)
            .NotEmpty()
            .IsInEnum().WithMessage("Invalid gender value.");

        RuleFor(x => x.DateOfBirth)
            .NotEmpty();
    }
}

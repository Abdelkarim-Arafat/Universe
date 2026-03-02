using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.AcademicProgramServices.Commands.AddAcademicProgram;

public class UpdateAcademicProgramCommandValidator : AbstractValidator<AddAcademicProgramCommand>
{
    public UpdateAcademicProgramCommandValidator()
    {
        RuleFor(d => d.Name)
            .NotEmpty()
            .WithMessage("AcademicProgram name is required.")
            .MaximumLength(100)
            .WithMessage("AcademicProgram name cannot exceed 100 characters.");

        RuleFor(d => d.Code)
            .NotEmpty()
            .WithMessage("AcademicProgram code is required.")
            .MaximumLength(50)
            .WithMessage("AcademicProgram code cannot exceed 50 characters.");
        RuleFor(d => d.Description)
            .MaximumLength(10000)
            .WithMessage("AcademicProgram description cannot exceed 10,000 characters.");

        RuleFor(d => d.RequiredCreditHours)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Required credit hours.");
    }
}

using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.AcademicYearAndSemestersServices.Commands.StartAcademicYear;

public class StartAcademicYearCommandValidator : AbstractValidator<StartAcademicYearCommand>
{
    public StartAcademicYearCommandValidator()
    {
        RuleFor(x => x.Name)
            .Matches(@"^\d{4}-\d{4}$")
            .WithMessage("Academic year name must be in the format YYYY-YYYY, e.g., 2026-2027.");

        RuleFor(x => x.StartDate)
            .NotNull()
            .Must(x => x >= DateOnly.FromDateTime(DateTime.UtcNow.Date)); 

        RuleFor(x => x.StartDate)
            .LessThan(x => x.EndDate)
            .WithMessage("StartDate must be less than or equal to EndDate.");

        RuleForEach(x => x.Semesters).SetValidator(new CreateSemesterDtoValidator());
    }
}
public class CreateSemesterDtoValidator : AbstractValidator<CreateSemesterDto>
{
    public CreateSemesterDtoValidator()
    {
        RuleFor(x => x.StartDate)
            .NotNull()
            .Must(x => x >= DateOnly.FromDateTime(DateTime.UtcNow.Date));

        RuleFor(x => x.StartDate)
            .LessThan(x => x.EndDate)
            .WithMessage("StartDate must be less than or equal to EndDate.");

        RuleFor(x => x.TermType)
            .IsInEnum().WithMessage("Invalid term type.");
    }
}
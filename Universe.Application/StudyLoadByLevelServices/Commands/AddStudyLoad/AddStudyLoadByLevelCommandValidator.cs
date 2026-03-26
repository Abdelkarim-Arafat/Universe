using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.StudyLoadByLevelServices.Commands.AddStudyLoad;

public class AddStudyLoadByLevelCommandValidator : AbstractValidator<AddStudyLoadByLevelCommand>
{
    public AddStudyLoadByLevelCommandValidator()
    {
        RuleFor(x => x.SemesterType)
            .NotEmpty().NotNull()
            .IsInEnum();

        RuleFor(x => x.MinHours)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.MaxHours)
            .GreaterThan(0);

        RuleFor(x => x)
            .Must(x => x.MaxHours >= x.MinHours)
            .WithMessage("MaxHours must be greater than or equal to MinHours");
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.StudyLoadByLevelServices.Commands.UpdateStudyLoad;

public class UpdateStudyLoadByLevelCommandValidator : AbstractValidator<UpdateStudyLoadByLevelCommand>
{
    public UpdateStudyLoadByLevelCommandValidator()
    {
        RuleFor(x => x.MinHours)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.MaxHours)
            .GreaterThan(0);

        RuleFor(x => x)
            .Must(x => x.MaxHours >= x.MinHours)
            .WithMessage("MaxHours must be greater than or equal to MinHours");
    }
}
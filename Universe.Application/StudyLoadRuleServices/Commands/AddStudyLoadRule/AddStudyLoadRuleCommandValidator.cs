using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.StudyLoadRuleServices.Commands.AddStudyLoadRule;

namespace Universe.Application.StudyLoadRuleServices.Commands.AddStudyLoadRule;

public class AddStudyLoadRuleCommandValidator : AbstractValidator<AddStudyLoadRuleCommand>
{
    public AddStudyLoadRuleCommandValidator()
    {
        RuleFor(x => x.GpaFrom)
            .GreaterThanOrEqualTo(0).WithMessage("GpaFrom must be greater than or equal to 0.");

        RuleFor(x => x.MinHours)
            .GreaterThanOrEqualTo(0).WithMessage("MinHours must be greater than or equal to 0.");

        RuleFor(x => x)
            .Must(x => x.GpaFrom <= x.GpaTo).WithMessage("GpaFrom must be less than or equal to GpaTo.");

        RuleFor(x => x)
            .Must(x => x.MinHours <= x.MaxHours).WithMessage("MinHours must be less than or equal to MaxHours.");
    }
}

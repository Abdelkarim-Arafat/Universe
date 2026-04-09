using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.UserServices.Commands.UpdateMilitaryData;

public class UpdateMilitaryDataCommandValidator : AbstractValidator<UpdateMilitaryDataCommand>
{
    public UpdateMilitaryDataCommandValidator()
    {

        RuleFor(x => x.MilitaryStatus)
            .NotEmpty().NotNull()
            .IsInEnum();

        RuleFor(x => x.MilitaryNumber)
            .MaximumLength(50);

        RuleFor(x => x.DecisionNumber)
            .MaximumLength(50);
    }
}

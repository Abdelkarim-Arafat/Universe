using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.AcademicEventServices.Commands.Add_Event;

public class AddEventCommandValidator : AbstractValidator<AddEventCommand>
{
    public AddEventCommandValidator()
    {
        RuleFor(x => x.Type)
             .NotEmpty()
             .IsInEnum();

        RuleFor(x => x.StartDate)
            .NotEmpty()
            .WithMessage("Start date is required.");

        RuleFor(x => x.EndDate)
            .NotEmpty()
            .GreaterThanOrEqualTo(x => x.StartDate)
            .WithMessage("End date must be greater than or equal to start date.");
    }
}

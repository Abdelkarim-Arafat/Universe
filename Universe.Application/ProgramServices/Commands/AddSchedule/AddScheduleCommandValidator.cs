using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.ProgramServices.Commands.AddSchedule;

internal class AddScheduleCommandValidator : AbstractValidator<AddScheduleCommand>
{
    public AddScheduleCommandValidator()
    {
        RuleFor(x => x.DayStartTime)
            .NotEmpty();

        RuleFor(x => x.DayEndTime)
            .NotEmpty()
            .GreaterThan(x => x.DayStartTime)
            .WithMessage("Day end time must be after start time.");

        RuleFor(x => x.SlotDurationMinutes)
            .GreaterThan(0)
            .WithMessage("Slot duration must be greater than 0.");

        RuleFor(x => x)
            .Must(x => (x.DayEndTime - x.DayStartTime).TotalMinutes % x.SlotDurationMinutes == 0)
            .WithMessage("Slot duration must divide the total day time without remainder.")
            .When(x => x.SlotDurationMinutes > 0);
            
    }
}

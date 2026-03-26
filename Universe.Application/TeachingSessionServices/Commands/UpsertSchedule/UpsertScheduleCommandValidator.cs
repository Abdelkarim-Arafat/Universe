using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.TeachingSessionServices.Commands.UpsertSchedule;

namespace Universe.Application.TeachingSessionServices.Commands.UpsertSchedule;

public class UpsertScheduleCommandValidator : AbstractValidator<UpsertScheduleCommand>
{
    public UpsertScheduleCommandValidator()
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

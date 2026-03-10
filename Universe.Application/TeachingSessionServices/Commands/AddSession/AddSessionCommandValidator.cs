using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.TeachingSessionServices.Commands.AddSession;

internal class AddSessionCommandValidator : AbstractValidator<AddSessionCommand>
{
    public AddSessionCommandValidator()
    {
        RuleFor(x => x.CourseOfferingId)
            .NotEmpty();

        RuleFor(x => x.InstructorId)
            .NotEmpty();

        RuleFor(x => x.RoomId)
            .NotEmpty();

        RuleFor(x => x.StartTime)
            .NotEmpty();

        RuleFor(x => x.EndTime)
            .NotEmpty()
            .GreaterThan(x => x.StartTime)
            .WithMessage("End time must be greater than start time.");

        RuleFor(x => x.Type)
            .NotEmpty()
            .IsInEnum();

        RuleFor(x => x.Day)
            .NotEmpty()
            .IsInEnum();

        RuleFor(x => x.GroupNumber)
            .GreaterThan(0)
            .WithMessage("Group number must be greater than zero.");

        RuleFor(x => x)
            .Must(x => (x.EndTime - x.StartTime).TotalMinutes > 0);
    }
}

namespace Universe.Application.RoomServices.Commands.Create;

public class CreateRoomCommandValidator : AbstractValidator<CreateRoomCommand>
{
    public CreateRoomCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Room name is required.")
            .MaximumLength(100).WithMessage("Building name cannot exceed 100 characters.");
        RuleFor(x => x.Capacity)
            .NotEmpty().WithMessage("Capacity is required.")
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x.RoomNumber)
           .NotEmpty().WithMessage("RoomNumber is required.")
           .GreaterThanOrEqualTo(1);    
    }
}


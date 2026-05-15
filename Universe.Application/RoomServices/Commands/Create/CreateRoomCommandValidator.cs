namespace Universe.Application.RoomServices.Commands.Create;

public class CreateRoomCommandValidator : AbstractValidator<CreateRoomCommand>
{
    public CreateRoomCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Room name is required.")
            .MaximumLength(30).WithMessage("Room name cannot exceed 30 characters.");

        RuleFor(x => x.Capacity)
            .InclusiveBetween(1, 1000).WithMessage("Capacity must be between 1 and 1000 students.");

        RuleFor(x => x.RoomNumber)
           .GreaterThan(0).WithMessage("Room number must be a positive value.");

        RuleFor(x => x.RoomType)
            .IsInEnum().WithMessage("Invalid Room Type selection.");
    }
}


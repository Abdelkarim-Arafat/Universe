namespace Universe.Application.RoomServices.Commands.Update;

public class UpdateRoomCommandValidator : AbstractValidator<UpdateRoomCommand>
{
    public UpdateRoomCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Room name is required.")
            .MaximumLength(30).WithMessage("Room name must be between 1 and 30 characters.");

        RuleFor(x => x.Capacity)
            .InclusiveBetween(1, 1000).WithMessage("Capacity must be between 1 and 1000 students.");

        RuleFor(x => x.RoomNumber)
            .GreaterThan(0).WithMessage("Room number must be a positive value.");

        RuleFor(x => x.RoomType)
            .IsInEnum().WithMessage("Invalid Room Type selection.");

        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Room Id is required.");
    }
}

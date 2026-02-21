namespace Universe.Application.RoomTypeServices.Commands.CreateRoomType;

public class CreateRoomTypeCommandValidator : AbstractValidator<CreateRoomTypeCommand>
{
    public CreateRoomTypeCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("RoomType name is required.")
            .MaximumLength(100).WithMessage("Building name cannot exceed 100 characters.");
    }
}

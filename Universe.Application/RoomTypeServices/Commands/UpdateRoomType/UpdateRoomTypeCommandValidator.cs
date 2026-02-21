
namespace Universe.Application.RoomTypeServices.Commands.UpdateRoomType;

public class UpdateRoomTypeCommandValidator : AbstractValidator<UpdateRoomTypeCommand>
{
    public UpdateRoomTypeCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("RoomType name is required.")
            .MaximumLength(100).WithMessage("Building name cannot exceed 100 characters.");
    }
}

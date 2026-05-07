namespace Universe.Application.BuildingServices.Commands.Create;


public class CreateBuildingCommandValidator : AbstractValidator<CreateBuildingCommand>
{
    public CreateBuildingCommandValidator()
    {
        RuleFor(x => x.Name)
              .NotEmpty().WithMessage("Building name is required.")
              .MinimumLength(3).WithMessage("Building name must be at least 3 characters.")
              .MaximumLength(100).WithMessage("Building name cannot exceed 100 characters.");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Building code is required.")
            .MaximumLength(20).WithMessage("Building code cannot exceed 20 characters.")
            .Matches(@"^[a-zA-Z0-9]+$").WithMessage("Building code must be alphanumeric (Letters and Numbers only).");
    }
}


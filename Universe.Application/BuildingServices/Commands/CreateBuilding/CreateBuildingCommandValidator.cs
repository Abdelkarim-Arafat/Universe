namespace Universe.Application.BuildingServices.Commands.CreateBuilding;


public class CreateBuildingCommandValidator : AbstractValidator<CreateBuildingCommand>
{
    public CreateBuildingCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Building name is required.")
            .MaximumLength(100).WithMessage("Building name cannot exceed 100 characters.");
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Building code is required.")
            .MaximumLength(5).WithMessage("Building code cannot exceed 5 characters.");
    }
}


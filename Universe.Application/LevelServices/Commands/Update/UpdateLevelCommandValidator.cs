namespace Universe.Application.LevelServices.Commands.Update;
public class UpdateLevelCommandValidator : AbstractValidator<UpdateLevelCommand>
{
    public UpdateLevelCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Level name is required.")
            .MaximumLength(50).WithMessage("Level name cannot exceed 50 characters.");

        RuleFor(x => x.MinHours)
           .LessThan(x => x.MaxHours)
           .WithMessage("MinHours must be less than MaxScore.");
        RuleFor(x => x.MinHours)
            .GreaterThanOrEqualTo(0)
            .WithMessage("MinHours must be greater than or equal to 0.");
    }
 
 
}


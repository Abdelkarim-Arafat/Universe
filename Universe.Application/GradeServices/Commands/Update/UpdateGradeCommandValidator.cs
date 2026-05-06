namespace Universe.Application.GradeServices.Commands.Update;

public class UpdateGradeCommandValidator : AbstractValidator<UpdateGradeCommand>
{
    public UpdateGradeCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Grade name is required.")
            .MaximumLength(100).WithMessage("Grade name cannot exceed 100 characters.");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Grade code is required.")
            .Matches(@"^[A-F][+-]?$").WithMessage("Code should be 1 or 2 chars (e.g., A, A+, B-).");

       
        RuleFor(x => x.MinScore)
            .InclusiveBetween(0, 100).WithMessage("MinScore must be between 0 and 100.")
            .LessThan(x => x.MaxScore).WithMessage("MinScore must be less than MaxScore.");

        RuleFor(x => x.MaxScore)
            .InclusiveBetween(0, 100).WithMessage("MaxScore must be between 0 and 100.");

   
        RuleFor(x => x.MinGradePoint)
            .InclusiveBetween(0, 4).WithMessage("MinGradePoint must be between 0 and 4.")
            .LessThanOrEqualTo(x => x.MaxGradePoint).WithMessage("MinGradePoint must be less than or equal to MaxGradePoint.");

        RuleFor(x => x.MaxGradePoint)
            .InclusiveBetween(0, 4).WithMessage("MaxGradePoint must be between 0 and 4.");
    }
}


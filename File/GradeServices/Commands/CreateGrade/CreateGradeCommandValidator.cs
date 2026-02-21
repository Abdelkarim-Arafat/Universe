namespace Universe.Application.GradeServices.Commands.CreateGrade;

public class CreateGradeCommandValidator : AbstractValidator<CreateGradeCommand>
{
    public CreateGradeCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Grade name is required.")
            .MaximumLength(100).WithMessage("Grade name cannot exceed 100 characters.");
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Grade code is required.")
            .MaximumLength(2).WithMessage("Grade code cannot exceed 2 characters.");

        RuleFor(x => x.MinScore)
            .LessThan(x => x.MaxScore)
            .WithMessage("MinScore must be less than MaxScore.");
        RuleFor(x => x.MaxScore)
          .GreaterThanOrEqualTo(0)
          .WithMessage("MinScore must be greater than or equal to 0.");
        RuleFor(x => x)
            .Must(ValidCode)
            .WithMessage("Code should be 1 or 2 chars with uppercase letters and (+ or -) symbol");
    }
    private static bool ValidCode(CreateGradeCommand command)
    {
        string code = command.Code;
        if (string.IsNullOrEmpty(code))
            return false;
        if (code.Length > 2)
            return false;
        if (code.Length <= 1)
        {
            if (!char.IsUpper(code[0]))
                return false;
        }
        if (code.Length == 2)
        {
            if (!(code[1] == '+' || code[1] == '-'))
                return false;
        }
        return true;
    }
}


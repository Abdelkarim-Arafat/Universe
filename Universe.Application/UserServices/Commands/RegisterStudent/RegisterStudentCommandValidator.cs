namespace Universe.Application.UserServices.Commands.RegisterStudent;

public class RegisterStudentCommandValidator : AbstractValidator<RegisterStudentCommand>
{
    public RegisterStudentCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

        RuleFor(x => x.UserName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Password)
            .NotEmpty()
            .Length(8, 15);

        RuleFor(x => x.StudentCode)
            .NotEmpty().WithMessage("Student code is required.")
            .MaximumLength(20).WithMessage("Student code cannot exceed 20 characters.");

        RuleFor(x => x.NationalIdOrPassport)
            .NotEmpty();
    }
}
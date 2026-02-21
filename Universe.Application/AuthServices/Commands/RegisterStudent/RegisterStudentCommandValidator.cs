
namespace Universe.Application.AuthServices.Commands.Register;

public class RegisterStudentCommandValidator : AbstractValidator<RegisterStudentCommand>
{
    public RegisterStudentCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.UserName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Password)
            .NotEmpty()
            .Length(8, 15);

        RuleFor(x => x.StudentCode)
            .NotEmpty();

        RuleFor(x => x.NationalId)
            .NotEmpty();
    }
}
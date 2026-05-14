namespace Universe.Application.ControlServices.Commands.UpsertStudentDegree;

public class UpsertStudentDegreeCommandValidator : AbstractValidator<UpsertStudentDegreeCommand>
{
    public UpsertStudentDegreeCommandValidator()
    {
       RuleFor(x=>x.Degree)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Degree must be greater than or equal to 0.");
    }
}

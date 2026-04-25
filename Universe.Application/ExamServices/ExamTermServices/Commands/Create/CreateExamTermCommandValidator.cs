namespace Universe.Application.ExamServices.ExamTermServices.Commands.Create;

public class CreateExamTermCommandValidator : AbstractValidator<CreateExamTermCommand>
{
    public CreateExamTermCommandValidator()
    {
        RuleFor(x => x.StartDate)
            .NotEmpty()
            .LessThanOrEqualTo (x => x.EndDate)
            .WithMessage("Startdate should be less than endDate");

        RuleFor(x => x.EndDate)
            .NotEmpty();
    }
}
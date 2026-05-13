namespace Universe.Application.ExamTermServices.Commands.Update;

public class UpdateExamTermCommandValidator : AbstractValidator<UpdateExamTermCommand>
{
    public UpdateExamTermCommandValidator()
    {
        RuleFor(x => x.StartDate)
           .NotEmpty()
           .LessThanOrEqualTo(x => x.EndDate)
           .WithMessage("Startdate should be less than endDate");

        RuleFor(x => x.EndDate)
            .NotEmpty();
    }
}
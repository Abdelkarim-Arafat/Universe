namespace Universe.Application.ExamCommitteeServices.Commands.Create;

public class CreateExamCommitteeCommandValidator : AbstractValidator<CreateExamCommitteeCommand>
{
    public CreateExamCommitteeCommandValidator()
    {
        RuleFor(com => com.MaxCapacity)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("capacity should be graeter than 0");
        RuleFor(com => com.CommitteeNumber)
           .NotEmpty()
           .GreaterThan(0)
           .WithMessage("Committee number should be graeter than 0");
    }
}
namespace Universe.Application.ExamCommitteeServices.Commands.Update;

public class UpdateExamCommitteeCommandValidator : AbstractValidator<UpdateExamCommitteeCommand>
{
    public UpdateExamCommitteeCommandValidator()
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
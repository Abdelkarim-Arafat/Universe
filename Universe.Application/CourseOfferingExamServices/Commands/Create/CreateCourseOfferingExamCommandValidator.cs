namespace Universe.Application.CourseOfferingExamServices.Commands.Create;

public class CreateCourseOfferingExamCommandValidator : AbstractValidator<CreateCourseOfferingExamCommand>
{
    public CreateCourseOfferingExamCommandValidator()
    {
        RuleFor(x => x.StartTime)
           .NotEmpty()
           .LessThan(x => x.EndTime)
           .WithMessage("Start time must be before end time.");

        RuleFor(x => x.Date)
            .NotEmpty();

        RuleFor(x => x.ExamCommitteesIds)
            .Must(x => x.Count != 0)
            .WithMessage("At least one exam committee must be selected.");

        RuleFor(x => x.ExamCommitteesIds)
            .Must(ids => ids.Distinct().Count() == ids.Count)
            .When(x => x.ExamCommitteesIds.Count > 0)
            .WithMessage("Exam committee IDs must be unique. Duplicates are not allowed.");
    }
}
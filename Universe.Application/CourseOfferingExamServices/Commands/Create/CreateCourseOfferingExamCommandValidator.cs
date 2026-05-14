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
              .Cascade(CascadeMode.Stop)
              .NotEmpty()
              .WithMessage("At least one exam committee must be selected.")
              .Must(ids => ids.Distinct().Count() == ids.Count)
              .WithMessage("Exam committee IDs must be unique. Duplicates are not allowed.");
    }
}
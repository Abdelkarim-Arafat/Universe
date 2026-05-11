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
    }
}
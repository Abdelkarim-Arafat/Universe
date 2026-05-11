namespace Universe.Application.CourseOfferingExamServices.Commands.Update;

public class UpdateCourseOfferingExamCommandValidator : AbstractValidator<UpdateCourseOfferingExamCommand>
{
    public UpdateCourseOfferingExamCommandValidator()
    {
        RuleFor(x => x.StartTime)
           .NotEmpty()
           .LessThan(x => x.EndTime)
           .WithMessage("Start time must be before end time.");

        RuleFor(x => x.Date)
            .NotEmpty();
    }
}
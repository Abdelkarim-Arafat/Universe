namespace Universe.Application.EnrollmentServices.Commands.Update;

public class UpdateEnrollmentCommandValidator : AbstractValidator<UpdateEnrollmentCommand>
{
    public UpdateEnrollmentCommandValidator()
    {
       
        RuleFor(x => x.StudentId)
            .NotEmpty().WithMessage("Student identifier is required.");

 
        RuleFor(x => x.SemesterId)
            .NotEmpty().WithMessage("Semester identifier is required.");

        
        RuleFor(x => x.newSessions)
            .NotNull().WithMessage("Sessions list cannot be null.")
            .Must(x => x.Count > 0).WithMessage("At least one session must be provided.")
            .Must(x => x.Select(s => s.SessionId).Distinct().Count() == x.Count)
            .WithMessage("Duplicate sessions are not allowed in the same request.");

        RuleForEach(x => x.newSessions)
            .SetValidator(new SessionAndCourseOfferingIdsValidator());
    }
}

public class SessionAndCourseOfferingIdsValidator : AbstractValidator<SessionAndCourseOfferingIds>
{
    public SessionAndCourseOfferingIdsValidator()
    {
        RuleFor(x => x.SessionId)
            .NotEmpty().WithMessage("SessionId is required for each entry.");

        RuleFor(x => x.CourseOfferingId)
            .NotEmpty().WithMessage("CourseOfferingId is required for each entry.");
    }
}

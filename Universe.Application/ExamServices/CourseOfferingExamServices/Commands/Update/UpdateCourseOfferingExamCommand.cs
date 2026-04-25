namespace Universe.Application.CourseOfferingExamServices.Commands.Update;

public record UpdateCourseOfferingExamCommand
(
    [Required] Guid Id,
     DateOnly Date,
     TimeOnly StartTime,
     TimeOnly EndTime
) : IRequest<Result>;
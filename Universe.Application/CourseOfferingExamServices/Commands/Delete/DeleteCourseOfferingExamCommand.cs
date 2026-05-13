namespace Universe.Application.CourseOfferingExamServices.Commands.Delete;

public record DeleteCourseOfferingExamCommand
(
    [Required] Guid Id
) : IRequest<Result>;
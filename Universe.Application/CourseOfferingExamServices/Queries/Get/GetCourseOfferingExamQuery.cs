namespace Universe.Application.CourseOfferingExamServices.Queries.Get;

public record GetCourseOfferingExamQuery
(
    [Required] Guid id
) : IRequest<Result<CourseOfferingExamResponse>>;
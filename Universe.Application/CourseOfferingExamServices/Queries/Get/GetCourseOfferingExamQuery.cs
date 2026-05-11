using Universe.Application.CourseOfferingExamServices.Dtos;
namespace Universe.Application.CourseOfferingExamServices.Queries.Get;

public record GetCourseOfferingExamQuery
(
    [Required] Guid id,
    FilterRequest Filter
) : IRequest<Result<CourseOfferingExamResponse>>;
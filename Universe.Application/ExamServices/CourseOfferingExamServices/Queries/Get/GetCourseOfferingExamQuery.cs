using Universe.Application.CourseOfferingExamServices.Dtos;
namespace Universe.Application.CourseOfferingExamServices.Queries.Get;

public record GetCourseOfferingExamQuery
(
    [Required] Guid Id
) : IRequest<Result<CourseOfferingExamResponse>>;
using Universe.Application.CourseOfferingExamServices.Dtos;
namespace Universe.Application.CourseOfferingExamServices.Queries.Get;

public record GetCourseOfferingExamQuery
(
    [Required] Guid courseOfferingId,
    [Required] Guid examTermId,
    FilterRequest Filter
) : IRequest<Result<CourseOfferingExamResponse>>;
using Universe.Core.Contracts.CourseOffering;

namespace Universe.Application.CourseOfferingServices.Queries.GetCourseOffering;

public record GetCourseOfferingQuery(
    [Required] Guid Id
) : IRequest<Result<CourseOfferingWithDetailsResponse>>;
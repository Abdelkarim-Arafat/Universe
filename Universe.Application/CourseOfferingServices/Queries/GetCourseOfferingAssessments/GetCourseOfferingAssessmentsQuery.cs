
using Universe.Core.Contracts.CourseOfferings;

namespace Universe.Application.CourseOfferingServices.Queries.GetCourseOfferingAssessments;

public record GetCourseOfferingAssessmentsQuery([Required] Guid CourseOfferingId)
    : IRequest<Result<List<CourseOfferingAssessmentResponse>>>;


using Universe.Core.Contracts.CourseOffering;

namespace Universe.Application.CourseOfferingServices.Queries.GetCourseOfferingAssessments;

public record GetCourseOfferingAssessmentsQuery([Required] Guid CourseOfferingId)
    : IRequest<Result<List<CourseOfferingAssessmentResponse>>>;

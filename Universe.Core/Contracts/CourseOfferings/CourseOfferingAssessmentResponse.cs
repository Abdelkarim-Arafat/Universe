
using Universe.Core.Enums;

namespace Universe.Core.Contracts.CourseOfferings;

public record CourseOfferingAssessmentResponse(
    Guid Id,
    AssessmentType Type,
    decimal MaxScore
);
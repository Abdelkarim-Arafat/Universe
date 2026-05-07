
using Universe.Core.Enums;

namespace Universe.Core.Contracts.CourseOffering;

public record CourseOfferingAssessmentResponse(
    Guid Id,
    AssessmentType Type,
    decimal MaxScore
);
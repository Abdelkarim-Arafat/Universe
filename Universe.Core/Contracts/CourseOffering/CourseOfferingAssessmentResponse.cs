
using Universe.Core.Enums;

namespace Universe.Core.Contracts.CourseOffering;

public record CourseOfferingAssessmentResponse(
    List<AssessmentDto> Assessments,
    decimal CourseTotalGrade
);
public record AssessmentDto
 (
     Guid Id,
     AssessmentType Type,
     decimal MaxScore
);
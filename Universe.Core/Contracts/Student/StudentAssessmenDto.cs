using Universe.Core.Entities;

namespace Universe.Core.Contracts.Student;

public record StudentAssessmenDto(
    decimal MaxScore,
    StudentAssessment? Assessment
);

public record CourseOfferingData(
     bool IsCourseOpenForControl,
    decimal SuccessPercentage,
    Guid CourseOfferingId
);

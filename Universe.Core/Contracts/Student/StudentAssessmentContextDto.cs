using Universe.Core.Entities;

namespace Universe.Core.Contracts.Student;

public record StudentAssessmentContextDto(
    bool IsAcademicProgramValid,
    bool IsCourseOpenForControl,
    decimal SuccessPercentage,
    decimal MaxScore,
    Guid CourseOfferingId,
    Enrollment? EnrollmentToUpdate,
    StudentAssessment? AssessmentToUpdate
);

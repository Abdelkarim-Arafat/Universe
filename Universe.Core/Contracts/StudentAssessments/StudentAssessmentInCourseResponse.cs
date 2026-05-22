namespace Universe.Core.Contracts.StudentAssessments;

public record StudentAssessmentInCourseResponse
(
    string AssessmentName,
    decimal MaxScore,
    decimal StudentScore
);
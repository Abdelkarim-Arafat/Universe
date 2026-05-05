using Universe.Core.Enums;

namespace Universe.Core.Contracts.CourseOfferings;

public record CourseOfferingWithDetailsResponse(
    Guid Id,
    int NumberOfGroups,
    decimal CreditHours,
    decimal TotalGrade,
    decimal SuccessPercentage,
    bool IsOptional,
    string? OptionalGroupCode,
    bool IsIncludedInGpa,
    RequirementType Type,
    TermType SemesterType,
    Guid CourseId,
    Guid SemesterId,
    Guid AcademicProgramId,
    Guid LevelId,
    List<CourseOfferingAssessmentResponse> Assessments
);
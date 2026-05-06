
using Universe.Core.Contracts.CourseOffering;
using Universe.Core.Enums;

namespace Universe.Application.CourseOfferingServices.Commands.AddCourseOffering;

public record AddCourseOfferingCommand(
    [Required] Guid AcademicProgramId,
    [Required] Guid AcademicYearId,
    int NumberOfGroups,
    decimal CreditHours,
    decimal TotalGrade,
    decimal SuccessPercentage,
    bool IsOptional,
    string? OptionalGroupCode,
    bool IsIncludedInGpa,
    Guid CourseId,
    TermType SemesterType,
    Guid LevelId,
    List<AddAssessments> Assessments
) : IRequest<Result<CourseOfferingWithDetailsResponse>>;

public record AddAssessments(
    AssessmentType Type,
    decimal MaxScore
);
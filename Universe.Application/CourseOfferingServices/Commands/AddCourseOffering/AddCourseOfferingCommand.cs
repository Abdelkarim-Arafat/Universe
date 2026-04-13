
using Universe.Application.CourseOfferingServices.Dtos;
using Universe.Core.Enums;

namespace Universe.Application.CourseOfferingServices.Commands.AddCourseOffering;

public record AddCourseOfferingCommand(
    [Required] Guid AcademicProgramId,
    [Required] Guid AcademicYearId,
    decimal CreditHours,
    decimal TotalGrade,
    decimal SuccessPercentage,
    bool IsOptional,
    string? OptionalGroupCode,
    bool IsIncludedInGpa,
    RequirementType Type,
    Guid CourseId,
    TermType SemesterType,
    Guid LevelId,
    List<CourseOfferingAssessmentCommand> Assessments
) : IRequest<Result<CourseOfferingWithDetailsResponse>>;

public record CourseOfferingAssessmentCommand(
    AssessmentType Type,
    decimal MaxScore
);
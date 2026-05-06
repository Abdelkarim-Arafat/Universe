using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Enums;

namespace Universe.Core.Contracts.CourseOffering;

public record CourseOfferingWithDetailsResponse(
    Guid Id,
    int NumberOfGroups,
    decimal CreditHours,
    decimal TotalGrade,
    decimal SuccessPercentage,
    bool IsOptional,
    string? OptionalGroupCode,
    bool IsIncludedInGpa,
    TermType SemesterType,
    Guid CourseId,
    Guid SemesterId,
    Guid AcademicProgramId,
    Guid LevelId,
    List<CourseOfferingAssessmentResponse> Assessments
);
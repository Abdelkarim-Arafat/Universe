using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Enums;

namespace Universe.Application.CourseOfferingServices.Dtos;

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
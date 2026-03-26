using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Enums;

namespace Universe.Application.CourseOfferingServices.Dtos;

public record CourseOfferingWithDetailsResponse(
    Guid Id,
    decimal CreditHours,
    decimal TotalGrade,
    decimal SuccessPercentage,
    bool IsOptional,
    string? OptionalGroupCode,
    bool IsIncludedInGpa,
    RequirementType Type,
    Guid CourseId,
    Guid SemesterId,
    Guid AcademicProgramId,
    Guid LevelId,
    List<CourseOfferingAssessmentResponse> Assessments
);
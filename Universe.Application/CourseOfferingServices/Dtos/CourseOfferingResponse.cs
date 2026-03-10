using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Enums;

namespace Universe.Application.CourseOfferingServices.Dtos;

public record CourseOfferingResponse(
    Guid Id,
    decimal CreditHours,
    decimal TotalGrade,
    decimal SuccessPercentage,
    bool IsOptional,
    bool IsIncludedInGpa,
    RequirementType Type,
    Guid CourseId,
    Guid SemesterId,
    Guid AcademicProgramId,
    Guid LevelId,
    List<CourseOfferingAssessmentResponse> Assessments
);
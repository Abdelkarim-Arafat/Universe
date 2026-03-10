using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Enums;

namespace Universe.Application.CourseOfferingServices.Dtos;

public record CourseOfferingAssessmentResponse(
    Guid Id,
    AssessmentType Type,
    decimal MaxScore
);
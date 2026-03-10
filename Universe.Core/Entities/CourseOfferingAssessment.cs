using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities.Core;
using Universe.Core.Enums;

namespace Universe.Core.Entities;

public class CourseOfferingAssessment : BaseEntity
{
    public Guid Id { get; set; }
    public CourseOfferingAssessment(){ Id = Guid.CreateVersion7(); }
    public AssessmentType Type { get; set; }
    public Guid CourseOfferingId { get; set; }
    public CourseOffering CourseOffering { get; set; } = default!;
    public decimal MaxScore { get; set; }
}
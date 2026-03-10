using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities.Core;

namespace Universe.Core.Entities;

public class CourseOfferingSession : BaseEntity
{
    public Guid TeachingSessionId { get; set; }
    public TeachingSession TeachingSession { get; set; } = default!;

    public Guid CourseOfferingId { get; set; }
    public CourseOffering CourseOffering { get; set; } = default!;
}

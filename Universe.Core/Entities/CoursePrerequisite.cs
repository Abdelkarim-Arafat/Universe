using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities.Core;

namespace Universe.Core.Entities;

public class CoursePrerequisite : BaseEntity
{
    public Guid CourseId { get; set; }
    public Course Course { get; set; } = default!;

    public Guid PrerequisiteCourseId { get; set; }
    public Course PrerequisiteCourse { get; set; } = default!;
}

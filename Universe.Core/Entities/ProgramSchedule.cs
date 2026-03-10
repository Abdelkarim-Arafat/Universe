using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities.Core;

namespace Universe.Core.Entities;

public class ProgramSchedule : BaseEntity
{
    public Guid ProgramId { get; set; }
    public AcademicProgram Program { get; set; } = default!;
    public Guid SemesterId { get; set; }
    public Semester Semester { get; set; } = default!;
    public TimeOnly DayStartTime { get; set; }
    public TimeOnly DayEndTime { get; set; }
    public int SlotDurationMinutes { get; set; }
}

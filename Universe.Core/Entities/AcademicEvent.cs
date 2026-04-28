using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities.Core;
using Universe.Core.Enums;

namespace Universe.Core.Entities;

public class AcademicEvent : BaseEntity
{
    public Guid Id { get; set; }
    public AcademicEvent() { Id = Guid.CreateVersion7(); }

    public Guid SemesterId { get; set; }
    public Semester Semester { get; set; } = default!;
    public Guid ProgramId { get; set; }
    public AcademicProgram Program { get; set; } = default!;
    public Enums.EventType Type { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public bool IsActive { get; set; }
}
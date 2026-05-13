using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities.Core;

namespace Universe.Core.Entities;

public class StudentAcademicProgram : BaseEntity
{
    public Guid StudentId {  get; set; }
    public Student Student { get; set; } = default!;
    public Guid AcademicProgramId { get; set; }
    public AcademicProgram AcademicProgram { get; set; } = default!;

    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public bool Currently { get; set; } = true;
}

using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities.Core;

namespace Universe.Core.Entities;

public class AcademicProgram : BaseEntity
{
    public Guid Id { get; set; }
    public AcademicProgram() { Id = Guid.CreateVersion7(); }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public int RequiredCreditHours { get; set; }
    public Guid CollegeId { get; set; }
    public College College { get; set; } = default!;
    public ICollection<CourseOffering> CourseOfferings { get; set; } = [];
    public ICollection<ApplicationUser> Users { get; set; } = [];
    public ICollection<Grade> Grades { get; set; } = [];
    public ICollection<Level> Levels { get; set; } = [];
    public ICollection<StudyLoadRule> StudyLoadRules { get; set; } = [];
    public ICollection<ProgramSchedule> ProgramSchedules { get; set; } = [];
}

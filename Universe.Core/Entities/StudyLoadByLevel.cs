using Universe.Core.Entities.Core;

namespace Universe.Core.Entities;

public class StudyLoadByLevel : BaseEntity
{
    public Guid Id { get; set; }
    public StudyLoadByLevel() { Id = Guid.CreateVersion7(); }
    public int MinHours { get; set; }
    public int MaxHours { get; set; }
    public Guid LevelId { get; set; }
    public Level Level { get; set; } = default!;
    public Guid SemesterId { get; set; }
    public Semester Sememester { get; set; } = default!;
    public Guid AcademicProgramId { get; set; }
    public AcademicProgram AcademicProgram { get; set; } = default!;
}

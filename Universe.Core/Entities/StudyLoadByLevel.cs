using Universe.Core.Entities.Core;
using Universe.Core.Enums;

namespace Universe.Core.Entities;

public class StudyLoadByLevel : BaseEntity
{
    public Guid Id { get; set; }
    public StudyLoadByLevel() { Id = Guid.CreateVersion7(); }
    public int MinHours { get; set; }
    public int MaxHours { get; set; }
    public Guid LevelId { get; set; }
    public Level Level { get; set; } = default!;
    public TermType SemesterType { get; set; }
    public Guid AcademicProgramId { get; set; }
    public AcademicProgram AcademicProgram { get; set; } = default!;
}

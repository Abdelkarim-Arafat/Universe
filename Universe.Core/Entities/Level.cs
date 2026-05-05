using Universe.Core.Entities.Core;

namespace Universe.Core.Entities;
public class Level : BaseEntity
{
    public Guid Id { get; set; }
    public Level() { Id = Guid.CreateVersion7(); }
    public string Name { get; set; } = string.Empty;
    public int MinHours { get; set; }
    public int MaxHours { get; set; }
    public Guid AcademicProgramId { get; set; }
    public AcademicProgram AcademicProgram { get; set; } = default!;
    public ICollection<CourseOffering> CourseOfferings { get; set; } = [];
    public ICollection<StudyLoadByLevel> StudyLoadByLevels { get; set; } = [];
}

using Universe.Core.Entities.Core;

namespace Universe.Core.Entities;

public class Level : BaseEntity
{
    public Level() { Id = Guid.CreateVersion7(); }
    public Guid Id { get;private set; }
    public string Name { get; set; } = string.Empty;
    public int MinHours { get; set; }
    public int MaxHours { get; set; }
    public Guid AcademicProgramId { get; set; }
    public AcademicProgram AcademicProgram { get; set; } = default!;
}

using Universe.Core.Entities.Core;

namespace Universe.Core.Entities;

public class Grade : BaseEntity
{
    public Guid Id { get; private set; }
    public Grade() { Id = Guid.CreateVersion7(); }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public int MinScore { get; set; }
    public int MaxScore { get; set; }
    public decimal MinGradePoint { get; set; }
    public decimal MaxGradePoint { get; set; }
    public Guid AcademicProgramId { get; set; }
    public AcademicProgram AcademicProgram { get; set; } = default!;
}

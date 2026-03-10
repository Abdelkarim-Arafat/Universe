
using Universe.Core.Entities.Core;

namespace Universe.Core.Entities;

public class StudyLoadRule : BaseEntity
{
    public Guid Id { get; set; }
    public StudyLoadRule(){ Id = Guid.CreateVersion7(); }
    public int MinHours { get; set; }
    public int MaxHours { get; set; }
    public decimal GpaFrom { get; set; }
    public decimal GpaTo { get; set; }
    public Guid AcademicProgramId { get; set; }
    public AcademicProgram AcademicProgram { get; set; } = default!;
}
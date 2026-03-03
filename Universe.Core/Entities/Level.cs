using System.Diagnostics;
using Universe.Core.Entities.Core;

namespace Universe.Core.Entities;

public class Level : BaseEntity
{
    public Level() { Id = Guid.CreateVersion7(); }
    public Guid Id { get;private set; }
    public string Name { get; set; } = string.Empty;
    public int MinHours { get; set; }
    public int MaxHours { get; set; }
<<<<<<< HEAD
    public Guid AcademicProgramId { get; set; }
=======
    public ICollection<CourseOffering> CourseOfferings { get; set; } = [];
    public Guid AcademicProgramd { get; set; }
>>>>>>> 3558604 (Add CourseOffering)
    public AcademicProgram AcademicProgram { get; set; } = default!;
}

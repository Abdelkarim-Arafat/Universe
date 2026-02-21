using Universe.Core.Entities.Core;

namespace Universe.Core.Entities;

public class Grade : BaseEntity
{
    public Grade() { Id = Guid.CreateVersion7(); }
    public Guid Id { get;private set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public int MinScore { get; set; }
    public int MaxScore { get; set; }
    public Guid CollegeId { get; set; }
    public College College { get; set; } = default!;
}

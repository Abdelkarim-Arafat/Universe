using Universe.Core.Entities.Core;
using Universe.Core.Enums;

namespace Universe.Core.Entities;

public class Room : BaseEntity
{
    public Guid Id { get; set; }
    public Room() => Id = Guid.CreateVersion7();
    public string Name { get; set; } = string.Empty;
    public int RoomNumber { get; set; }
    public int Capacity { get; set; }
    public RoomType RoomType { get; set; } = default!;
    public Guid BuildingId { get; set; }
    public Building Building { get; set; } = default!;
    public ICollection<TeachingSession> TeachingSessions { get; set; } = [];
    public ICollection<ExamCommittee> ExamCommittees { get; set; } = [];
}

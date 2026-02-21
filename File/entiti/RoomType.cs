using Universe.Core.Entities.Core;

namespace Universe.Core.Entities;

public class RoomType : BaseEntity
{
    public Guid Id { get; set; }
    public RoomType() { Id = Guid.CreateVersion7(); }
    public string Name { get; set; } = string.Empty;
    public ICollection<Room> Rooms { get; set; } = [];
}

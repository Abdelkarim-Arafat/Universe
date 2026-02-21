using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities.Core;

namespace Universe.Core.Entities;

public class Room : BaseEntity
{
    public Guid Id { get; set; }
    public Room() => Id = Guid.CreateVersion7();
    public string Name { get; set; }
    public int RoomNumber { get; set; }
    public int Capacity { get; set; }
    public Guid RoomTypeId { get; set; }
    public RoomType RoomType { get; set; } = default!;
    public Guid BuildingId { get; set; }
    public Building Building { get; set; } = default!;
}

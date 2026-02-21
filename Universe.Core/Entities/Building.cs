using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities.Core;

namespace Universe.Core.Entities;

public class Building : BaseEntity
{
    public Guid Id { get; set; }
    public Building() { Id = Guid.CreateVersion7(); }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public ICollection<Room> Rooms { get; set; } = [];
}

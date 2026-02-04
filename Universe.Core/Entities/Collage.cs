using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities.Core;

namespace Universe.Core.Entities;

public class College : BaseEntity
{
    public Guid Id { get; set; }
    public College() { Id = Guid.CreateVersion7(); }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }

    public List<ApplicationUser> Users { get; set; } = [];
}

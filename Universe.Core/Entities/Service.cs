using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities.Core;

namespace Universe.Core.Entities;

public class Service : BaseEntity
{
    public Guid Id { get; set; }
    public Service(){ Id = Guid.CreateVersion7(); }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public Guid CollegeId { get; set; }
    public College College { get; set; } = default!;
    public ICollection<Payment> Payments { get; set; } = [];
}

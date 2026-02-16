using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities.Core;

namespace Universe.Core.Entities;

public class Department : BaseEntity
{
    public Guid Id { get; set; }
    public Department() { Id = Guid.CreateVersion7(); }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public int RequiredCreditHours { get; set; }
    public Guid CollegeId { get; set; }
    public College College { get; set; } = default!;
    public ICollection<ApplicationUser> Users { get; set; } = [];
}

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
    public decimal MaxGpa { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public ICollection<Student> Students { get; set; } = [];
    public ICollection<AcademicProgram> AcademicPrograms { get; set; } = [];
    public ICollection<Course> Courses { get; set; } = [];
    public ICollection<ApplicationUser> Users { get; set; } = [];
    public ICollection<AcademicYear> AcademicYears { get; set; } = [];
    public ICollection<Service> Services { get; set; } = [];
}

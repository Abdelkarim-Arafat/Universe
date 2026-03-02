using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities.Core;

namespace Universe.Core.Entities;

public class AcademicYear : BaseEntity
{
    public Guid Id { get; set; }
    public AcademicYear(){ Id = Guid.CreateVersion7(); }
    public string Name { get; set; } = default!; // 2025-2026
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public bool IsActive 
        => DateOnly.FromDateTime(DateTime.UtcNow) >= StartDate &&
            DateOnly.FromDateTime(DateTime.UtcNow) <= EndDate;

    public Guid CollegeId { get; set; }
    public College College { get; set; } = default!;
    public ICollection<Semester> Semesters { get; set; } = [];
}

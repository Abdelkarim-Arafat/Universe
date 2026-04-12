using Universe.Core.Entities.Core;

namespace Universe.Core.Entities;

public class StudentSemesterSummary : BaseEntity
{
    public Guid Id { get; set; }
    public StudentSemesterSummary() { Id = Guid.CreateVersion7(); }
    public decimal SemesterGPA { get; set; }
    public decimal CumulativeGPA { get; set; }
    public int AttemptedHours { get; set; }
    public int TotalHoursEarned { get; set; }
    public string SemesterGrade { get; set; } = string.Empty;
    public string CumulativeGrade { get; set; } = string.Empty;
    public bool IsFinalized { get; set; } = false;
    public Guid StudentId { get; set; }
    public Student Student { get; set; } = default!;
    public Guid SemesterId { get; set; }
    public Semester Semester { get; set; } = default!;
}
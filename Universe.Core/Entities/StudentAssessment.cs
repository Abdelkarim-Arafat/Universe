using Universe.Core.Entities.Core;

namespace Universe.Core.Entities;

public class StudentAssessment : BaseEntity
{
    public Guid Id { get; set; }
    public StudentAssessment() { Id = Guid.CreateVersion7(); }
    public decimal degree { get; set; } = 0;
    public Guid CourseOfferingId { get; set; }
    public Guid StudentId { get; set; }
    public Student Student { get; set; } = default!;
    public Guid CourseOfferingAssessmentId { get; set; }
    public CourseOfferingAssessment CourseOfferingAssessment { get; set; } = default!;
}

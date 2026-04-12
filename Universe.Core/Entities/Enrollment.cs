using Universe.Core.Entities.Core;
using Universe.Core.Enums;

namespace Universe.Core.Entities;

public class Enrollment : BaseEntity
{
    public Guid Id { get; set; }
    public Enrollment() { Id = Guid.CreateVersion7(); }
    public int GroupNumber { get; set; }
    public EnrollmentStatus Status { get; set; }
    public Guid CourseId { get; set; }
    public Guid StudentId { get; set; }
    public Student Student { get; set; } = default!;
    public Guid CourseOfferingId { get; set; }
    public CourseOffering CourseOffering { get; set; } = default!;
    public ICollection<TeachingSessionEnrollment> TeachingSessionEnrollments { get; set; } = [];
}

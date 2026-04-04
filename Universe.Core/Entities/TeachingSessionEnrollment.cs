using Universe.Core.Entities.Core;

namespace Universe.Core.Entities;

public class TeachingSessionEnrollment : BaseEntity
{
    public Guid TeachingSessionId { get; set; }
    public TeachingSession TeachingSession { get; set; } = default!;
    public Guid EnrollmentId { get; set; }
    public Enrollment Enrollment { get; set; } = default!;
}

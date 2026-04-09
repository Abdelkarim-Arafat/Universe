using Universe.Core.Entities;

namespace Universe.Core.Interfaces.Repositories;

public interface IEnrollmentRepository
{
    Task<Enrollment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> IsStudentPassedInCourse(Guid StudentId, Guid CourseId, CancellationToken cancellationToken);
    Task<int> NumberOfEnrollmentsInSession(Guid TeachingSessionId, CancellationToken cancellationToken);
    Task<int> AvailableSeatsInSession(Guid TeachingSessionId, CancellationToken cancellationToken);
    Task<List<Enrollment>> GetStudentEnrollmentsWithSessions(Guid StudentId, CancellationToken cancellationToken);
    Task<Dictionary<Guid, int>> GetOccupiedSeatsBulkAsync(List<Guid> SessionsId, CancellationToken cancellationToken);
    Task<List<(TeachingSession Session, int EnrolledCount)>> GetSessionsWithAvailabilityBulk(
     Guid courseOfferingId,
     CancellationToken cancellationToken);
    Task<List<Enrollment>> GetStudentEnrollmentsAsync(Guid studentId, CancellationToken cancellationToken);
    Task<List<TeachingSessionEnrollment>> GetTeachingSessionEnrollmentAsync(Guid StudentId, CancellationToken cancellationToken);
}

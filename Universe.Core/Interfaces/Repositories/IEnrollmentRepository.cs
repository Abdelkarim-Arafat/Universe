using Universe.Core.Entities;

namespace Universe.Core.Interfaces.Repositories;

public interface IEnrollmentRepository
{
    Task<Enrollment?> GetEnrollmentByStudentIdAndCourseOfferingIdAsync
        (Guid StudentId, Guid CourseOfferingId, CancellationToken cancellationToken);
    Task<List<Enrollment>> GetStudentEnrollmentsWithSessions(Guid StudentId, CancellationToken cancellationToken);
    Task<Dictionary<Guid, int>> GetOccupiedSeatsBulkAsync(List<Guid> SessionsId, CancellationToken cancellationToken);
    Task<List<(TeachingSession Session, int EnrolledCount)>> GetSessionsWithAvailabilityBulk(
     Guid courseOfferingId,
     CancellationToken cancellationToken);
    Task<List<Enrollment>> GetStudentEnrollmentsAsync(Guid studentId, CancellationToken cancellationToken);

    Task<List<TeachingSessionEnrollment>> 
        GetTeachingSessionEnrollmentAsync(Guid StudentId, CancellationToken cancellationToken);

    Task<decimal> CalculateRegistredHoursAsync(Guid studentId, CancellationToken cancellationToken);
    Task<List<Guid>> GetStudentsIdsByCourseOfferingAsync(Guid CourseOfferingId, CancellationToken CancellationToken);
}

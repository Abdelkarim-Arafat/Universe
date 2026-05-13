using Universe.Core.Contracts.Enrollments;
using Universe.Core.Contracts.Grades;
using Universe.Core.Contracts.Student;
using Universe.Core.Entities;

namespace Universe.Core.Interfaces.Repositories;

public interface IEnrollmentRepository
{
    Task<StudentAcademicHistoryContextDto>
        GetStudentAcademicHistoryContextAsync
        (Guid studentId, List<GradeResponse> letterDegrees, CancellationToken cancellationToken);

    Task<List<StudentExistingEnrollment>> GetStudentScheduleAsync
        (Guid studentId, Guid currentSemesterId, CancellationToken cancellationToken);
    Task<List<Enrollment>> GetExistingEnrollmentIncludingSessionsAsync
    (Guid studentId, Guid semesterId, CancellationToken cancellationToken);
    Task<List<Guid>> GetRegisteredCourseOfferingIdsInCurrentSemesterAsync(Guid studentId, Guid semesterId, CancellationToken cancellationToken);
    Task<Enrollment?> GetEnrollmentDataByCourseOfferingIdAsync(Guid courseOfferingId, Guid studentId, CancellationToken cancellationToken);
    Task<List<StudentExistingEnrollment>> GetExistingEnrollmentsInfoAsync(Guid studentId, Guid semesterId, CancellationToken cancellationToken);
    Task<decimal> CalculateCurrentRegisteredHoursAsync(Guid studentId, Guid semesterId, CancellationToken cancellationToken);
}

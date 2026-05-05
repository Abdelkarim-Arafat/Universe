using Universe.Core.Contracts.Enrollments;
using Universe.Core.Contracts.Student;
using Universe.Core.Entities;

namespace Universe.Core.Interfaces.Repositories;

public interface IEnrollmentRepository
{
    Task<StudentAcademicHistoryContextDto?> GetStudentAcademicHistoryContextAsync(Guid studentId, CancellationToken cancellationToken);

    Task<EnrollmentValidationContextDto?> 
        GetEnrollmentValidationContextAsync(Guid studentId, Guid semesterId,  CancellationToken cancellationToken);
    Task<List<StudentExistingEnrollment>> GetStudentScheduleAsync(Guid studentId, CancellationToken cancellationToken);
    Task<UpdateEnrollmentValidationDto?> GetUpdateEnrollmentValidationDataAsync
        (Guid studentId, Guid semesterId, List<Guid> courseOfferingIds, List<Guid> sessionIds, CancellationToken cancellationToken);
    Task<EnrollmentExecutionContextDto> GetEnrollmentExecutionDataAsync
        (Guid studentId, Guid semesterId, HashSet<Guid> incomingCourseOfferingIds, CancellationToken cancellationToken);
}

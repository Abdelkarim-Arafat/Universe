using Universe.Core.Dtos.Enrollments;
using Universe.Core.Dtos.Student;
using Universe.Core.Entities;

namespace Universe.Core.Interfaces.Repositories;

public interface IEnrollmentRepository
{

    Task<List<Guid>> GetStudentsIdsByCourseOfferingAsync(Guid CourseOfferingId, CancellationToken CancellationToken);
    Task<StudentAcademicHistoryContextDto?> GetStudentAcademicHistoryContextAsync(Guid studentId, CancellationToken cancellationToken);

    Task<EnrollmentValidationContextDto?> 
        GetEnrollmentValidationContextAsync(Guid studentId, Guid semesterId,  CancellationToken cancellationToken);
    Task<List<StudentExistingEnrollment>> GetStudentScheduleAsync(Guid studentId, CancellationToken cancellationToken);
    Task<UpdateEnrollmentValidationDto?> GetUpdateEnrollmentValidationDataAsync
        (Guid studentId, Guid semesterId, List<Guid> courseOfferingIds, List<Guid> sessionIds, CancellationToken cancellationToken);
    Task<EnrollmentExecutionContextDto> GetEnrollmentExecutionDataAsync
        (Guid studentId, Guid semesterId, HashSet<Guid> incomingCourseOfferingIds, CancellationToken cancellationToken);
}

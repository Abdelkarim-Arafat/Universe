using Universe.Core.Contracts.Student;
using Universe.Core.Entities;

namespace Universe.Core.Interfaces.Repositories;

public interface IUserRepository
{
    IQueryable<ApplicationUser> GetAllStaffAsync();
    Task<RefreshToken?> GetRefreshTokenAsync(string token, CancellationToken cancellationToken);
    Task<bool> IsStudentCodeExistsAsync(Guid CollegeId, Guid? StudentId, string studentCode, CancellationToken cancellationToken);
    Task<bool> IsStudentNationalIdExistsAsync(Guid CollegeId, Guid? StudentId, string studentCode, CancellationToken cancellationToken);
    Task<Student?> GetStudentByIdAsync(Guid UserId, CancellationToken cancellationToken);
    Task<List<Student>> GetStudentsByIdsAsync(
    List<Guid> studentIds,
    CancellationToken cancellationToken);
    Task<Guid?> GetCurrentProgram(Guid StudentId, CancellationToken cancellationToken);
    Task<bool> UserIsExistAsync(Guid Id, CancellationToken cancellationToken);

    Task<decimal> CalculateCreditHoursAsync(Guid StudentId, Guid? SemesterId, CancellationToken cancellationToken);
    Task<decimal> CalculateGpaAsync(Guid StudentId, Guid? SemesterId, CancellationToken cancellationToken);

    Task<List<StudentAssessment>> GetStudentAssessmentByCourseOfferingBulkAsync
        (List<Guid> ToRemoveCourses, Guid StudentId, CancellationToken cancellationToken);
    Task<List<(Student Student, string StudentLevelName, List<StudentAssessment> Assessments)>> GetStudentsByCourseOfferingAndGroupNumberAsync(
     Guid courseOfferingId,
     int? groupNumber,
     CancellationToken cancellationToken);

    Task<List<StudentAssessment>> GetStudentsAssessmentsAsync(List<Guid> StudentsIds, Guid CourseOfferingId, CancellationToken cancellationToken);

    Task<StudentExamsResponse> GetStudentExamsTablesAsync(Guid studentId, CancellationToken cancellationToken);
}

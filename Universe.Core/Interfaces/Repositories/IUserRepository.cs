using Microsoft.EntityFrameworkCore;
using Universe.Core.Contracts.Control;
using Universe.Core.Contracts.Student;
using Universe.Core.Contracts.User;
using Universe.Core.Entities;

namespace Universe.Core.Interfaces.Repositories;

public interface IUserRepository
{
    Task<GraduationDetailsResponse?> GetStudentGraduationDetailsAsync(Guid studentId, CancellationToken cancellationToken);
    Task<PersonalDataResponse?> GetStudentPersonalDataAsync(Guid studentId,CancellationToken cancellationToken = default);
    Task<ContactDataResponse?> GetStudentContactDataAsync(Guid studentId, CancellationToken cancellationToken = default);
    Task<ParentDataResponse?> GetStudentParentDataAsync(Guid studentId, CancellationToken cancellationToken = default);
    Task<MilitaryDataResponse?> GetStudentMilitaryDataAsync(Guid studentId, CancellationToken cancellationToken = default);
    Task<PreviousQualificationResponse?> GetStudentPreviousQualificationAsync(Guid studentId, CancellationToken cancellationToken = default);
    IQueryable<ApplicationUser> GetAllStaffAsync();
    Task<RefreshToken?> GetRefreshTokenAsync(string token, CancellationToken cancellationToken);
    Task<bool> IsStudentCodeExistsAsync(Guid CollegeId, Guid? StudentId, string studentCode, CancellationToken cancellationToken);
    Task<bool> IsStudentNationalIdExistsAsync(Guid CollegeId, Guid? StudentId, string studentCode, CancellationToken cancellationToken);
    Task<Student?> GetStudentByIdAsync(Guid UserId, CancellationToken cancellationToken);
    Task<List<Student>> GetStudentsByIdsAsync(List<Guid> studentIds, CancellationToken cancellationToken);
    Task<bool> UserIsExistAsync(Guid Id, CancellationToken cancellationToken);
    Task<decimal> CalculateCreditHoursAsync(Guid StudentId, Guid? SemesterId, CancellationToken cancellationToken);
    Task<decimal> CalculateGpaAsync(Guid StudentId, Guid? SemesterId, CancellationToken cancellationToken);
    Task<List<StudentAssessment>> GetStudentAssessmentByCourseOfferingBulkAsync
        (List<Guid> ToRemoveCourses, Guid StudentId, CancellationToken cancellationToken);
    Task<StudentExamsResponse> GetStudentExamsTablesAsync(Guid studentId, CancellationToken cancellationToken);
    Task<ILookup<Guid, StudentDegreeValue>> GetStudentsAssessmentsLookupAsync
        (List<Guid> StudentsIds, Guid CourseOfferingId, CancellationToken cancellationToken);
    Task<Dictionary<Guid, string>> GetStudentsLevelNameDictionaryAsync(List<Guid> StudentsIds, Guid programId, CancellationToken cancellationToken);
}

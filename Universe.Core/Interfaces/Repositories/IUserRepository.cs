using Microsoft.EntityFrameworkCore;
using Universe.Core.Contracts.Control;
using Universe.Core.Contracts.Student;
using Universe.Core.Contracts.User;
using Universe.Core.Entities;

namespace Universe.Core.Interfaces.Repositories;

public interface IUserRepository
{
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
    Task<bool> IsUserExistAsync(Guid Id, CancellationToken cancellationToken);
    Task<decimal> CalculateCreditHoursAsync(Guid StudentId, Guid? SemesterId, CancellationToken cancellationToken);
    Task<decimal> CalculateGpaAsync(Guid studentId, Guid? semesterId, Guid programId, CancellationToken cancellationToken);
    Task<List<StudentExam>> GetStudentExamsTablesAsync
    (Guid studentId, List<Guid> currentCoursesIds, List<Guid> examTermsIds, CancellationToken cancellationToken);
    Task<Dictionary<Guid, string>> GetStudentsLevelNameDictionaryAsync(List<Guid> StudentsIds, Guid programId, CancellationToken cancellationToken);
    Task<Guid?> GetStudentCollegeIdAsync(Guid studentId, CancellationToken cancellationToken = default);
}

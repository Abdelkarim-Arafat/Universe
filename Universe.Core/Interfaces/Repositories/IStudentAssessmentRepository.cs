using Universe.Core.Entities;
using Universe.Core.Contracts.Student;
using Universe.Core.Contracts.Control;
namespace Universe.Core.Interfaces.Repositories;

public interface IStudentAssessmentRepository
{
    Task<decimal> GetStudentDegreeInCourseAsync(Guid studentId, Guid courseOfferingId, CancellationToken cancellationToken);
    Task<StudentAssessmenDto?> GetAssessmentWithMaxScoreAsync(
          Guid studentId,
          Guid courseAssessmentId,
          CancellationToken cancellationToken);
    Task<List<StudentAssessment>> GetStudentAssessmentByCourseOfferingBulkAsync(List<Guid> ToRemoveCourses, Guid StudentId, CancellationToken cancellationToken);
    Task<ILookup<Guid, StudentDegreeValue>> GetStudentsAssessmentsLookupAsync(List<Guid> StudentsIds, Guid CourseOfferingId, CancellationToken cancellationToken);
}

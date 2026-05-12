using Universe.Core.Entities;
using Universe.Core.Contracts.Student;
namespace Universe.Core.Interfaces.Repositories;

public interface IStudentAssessmentRepository
{
    Task<decimal> GetStudentDegreeInCourseAsync(Guid studentId, Guid courseOfferingId, CancellationToken cancellationToken);
    Task<StudentAssessmenDto?> GetAssessmentWithMaxScoreAsync(
          Guid studentId,
          Guid courseAssessmentId,
          CancellationToken cancellationToken);
}

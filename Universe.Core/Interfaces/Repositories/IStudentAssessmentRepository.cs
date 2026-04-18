using Universe.Core.Entities;

namespace Universe.Core.Interfaces.Repositories;

public interface IStudentAssessmentRepository
{
    Task<decimal> GetStudentDegreeInCourseAsync(Guid studentId, Guid courseOfferingId, CancellationToken cancellationToken);
    Task<StudentAssessment?> GetStudentAssessmentAsync(Guid studentId, Guid courseAssessmentId, CancellationToken cancellationToken);
}

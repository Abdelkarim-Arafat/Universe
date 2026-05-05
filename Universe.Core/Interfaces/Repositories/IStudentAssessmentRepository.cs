using Universe.Core.Entities;
using Universe.Core.Dtos.Student;
namespace Universe.Core.Interfaces.Repositories;

public interface IStudentAssessmentRepository
{
    Task<decimal> GetStudentDegreeInCourseAsync(Guid studentId, Guid courseOfferingId, CancellationToken cancellationToken);
 
    Task<StudentAssessmentContextDto?>
        GetContextForDegreeUpsertAsync(Guid studentId, Guid courseAssessmentId, Guid academicProgramId, CancellationToken cancellationToken);
}

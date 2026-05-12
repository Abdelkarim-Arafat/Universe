using Microsoft.EntityFrameworkCore;
using Universe.Core.Contracts.Student;
using Universe.Core.Interfaces.Repositories;
using Universe.Infrastructure.Persistence;
 
namespace Universe.Infrastructure.Repositories;

public class StudentAssessmentRepository(ApplicationDbContext context) : IStudentAssessmentRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<decimal> GetStudentDegreeInCourseAsync
        (Guid studentId, Guid courseOfferingId, CancellationToken cancellationToken)
    {
        return await _context.StudentAssessments
            .AsNoTracking()
            .Where(sa => sa.StudentId == studentId
                         && sa.CourseOfferingAssessment.CourseOfferingId == courseOfferingId
                         && !sa.IsDeleted)
            .SumAsync(sa => sa.degree ?? 0, cancellationToken);
    }

    public async Task<StudentAssessmenDto?> GetAssessmentWithMaxScoreAsync(
        Guid studentId,
        Guid courseAssessmentId,
        CancellationToken cancellationToken)
    {
        return await _context.StudentAssessments
            .Where(sa => sa.StudentId == studentId && sa.CourseOfferingAssessmentId == courseAssessmentId)
            .Select(sa => new StudentAssessmenDto(
                sa.CourseOfferingAssessment.MaxScore,
                sa
              ))
            .FirstOrDefaultAsync(cancellationToken);
    }
}

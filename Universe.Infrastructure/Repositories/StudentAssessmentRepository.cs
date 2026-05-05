using Microsoft.EntityFrameworkCore;
using Universe.Core.Dtos.Student;
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
    
    public async Task<StudentAssessmentContextDto?> GetContextForDegreeUpsertAsync(
        Guid studentId,
        Guid courseAssessmentId,
        Guid academicProgramId,
        CancellationToken cancellationToken)
    {
        var data = await _context.StudentAssessments
            .Where(sa => sa.StudentId == studentId && sa.CourseOfferingAssessmentId == courseAssessmentId)
            .Select(sa => new StudentAssessmentContextDto(
                _context.AcademicPrograms.Any(p => p.Id == academicProgramId),
                sa.CourseOfferingAssessment.CourseOffering.IsOpenForControl, 
                sa.CourseOfferingAssessment.CourseOffering.SuccessPercentage, 
                sa.CourseOfferingAssessment.MaxScore,                       
                sa.CourseOfferingAssessment.CourseOfferingId,                
                _context.Enrollments
                .FirstOrDefault(e => 
                e.StudentId == studentId
                && e.CourseOfferingId == sa.CourseOfferingAssessment.CourseOfferingId),
                sa                                                           
            ))
            .FirstOrDefaultAsync(cancellationToken);

        return data;
    }
}

using Microsoft.EntityFrameworkCore;
using Universe.Core.Contracts.Control;
using Universe.Core.Contracts.Student;
using Universe.Core.Entities;
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

    public async Task<List<StudentAssessment>> GetStudentAssessmentByCourseOfferingBulkAsync
    (List<Guid> ToRemoveCourses, Guid StudentId, CancellationToken cancellationToken)
    {
        return await _context.StudentAssessments.
             Where(ass => ass.StudentId == StudentId
             && ToRemoveCourses.Contains(ass.CourseOfferingAssessment.CourseOfferingId)
             && !ass.IsDeleted)
             .ToListAsync(cancellationToken);
    }
    public async Task<ILookup<Guid, StudentDegreeValue>> GetStudentsAssessmentsLookupAsync(
     List<Guid> StudentsIds,
     Guid CourseOfferingId,
     CancellationToken cancellationToken)
    {
        var assessmentsList = await _context.StudentAssessments
            .Where(sa => StudentsIds.Contains(sa.StudentId)
                      && sa.CourseOfferingAssessment.CourseOfferingId == CourseOfferingId
                      && !sa.IsDeleted)
            .Select(sa => new
            {
                sa.StudentId,
                assessments = new StudentDegreeValue(
                  sa.CourseOfferingAssessmentId,
                  sa.degree
            )
            })
            .ToListAsync(cancellationToken);

        return assessmentsList.ToLookup(sa => sa.StudentId, sa => sa.assessments);
     }
}

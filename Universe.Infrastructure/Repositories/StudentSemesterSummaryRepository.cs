using Microsoft.EntityFrameworkCore;
using Universe.Core.Entities;
using Universe.Core.Interfaces.Repositories;
using Universe.Infrastructure.Persistence;

namespace Universe.Infrastructure.Repositories;

public class StudentSemesterSummaryRepository(ApplicationDbContext context) : IStudentSemesterSummaryRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<List<StudentSemesterSummary>> GetStudentSummariesAsync(Guid studentId, CancellationToken cancellationToken)
    {
        return await _context.StudentSemesterSummarys
         .AsNoTracking()
         .Include(s => s.Semester)
             .ThenInclude(sem => sem.AcademicYear)
         .Where(s => s.StudentId == studentId)
         .OrderBy(s => s.Semester.AcademicYear.StartDate)
         .ThenBy(s => s.Semester.StartDate)
         .ToListAsync();
    }
}

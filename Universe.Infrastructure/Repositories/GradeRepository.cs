using Microsoft.EntityFrameworkCore;
using Universe.Core.Entities;
using Universe.Core.Interfaces.Repositories;
using Universe.Infrastructure.Persistence;

namespace Universe.Infrastructure.Repositories;

public class GradeRepository(ApplicationDbContext context) : IGradeRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<List<Grade>> GetProgramGradesAsync(Guid AcademicProgramId, CancellationToken cancellationToken = default)
    {
        var grades = await _context.Grades
            .Where(grade => grade.AcademicProgramId == AcademicProgramId && !grade.IsDeleted)
            .OrderBy(grade => grade.MinScore)
            .ToListAsync(cancellationToken);
        return grades;
    }

    public async Task<Grade?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Grades.FirstOrDefaultAsync(grade => grade.Id == id && !grade.IsDeleted, cancellationToken);
    }
    public async Task<bool> CheckOverLabedScoresAsync(int MinScore, int MaxScore, Guid AcademicProgramId, CancellationToken cancellationToken = default)
    {
        return await _context.Grades
            .AnyAsync(g => (!(g.MinScore > MaxScore || g.MaxScore < MinScore))
            && !g.IsDeleted
            && g.AcademicProgramId == AcademicProgramId, cancellationToken);
    }

    public async Task<bool> CheckOverLabedScoresAsync(int MinScore, int MaxScore, Guid Id, Guid AcademicProgramId, CancellationToken cancellationToken = default)
    {
        return await _context.Grades
           .AnyAsync(g => (!(g.MinScore > MaxScore || g.MaxScore < MinScore))
           && !g.IsDeleted
           && g.Id != Id
           && g.AcademicProgramId == AcademicProgramId, cancellationToken);
    }
}

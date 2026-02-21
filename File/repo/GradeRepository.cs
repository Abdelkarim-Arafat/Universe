using Microsoft.EntityFrameworkCore;
using Universe.Core.Abstractions;
using Universe.Core.Entities;
using Universe.Core.Errors;
using Universe.Core.Interfaces.Repositories;
using Universe.Infrastructure.Persistence;

namespace Universe.Infrastructure.Repositories;

public class GradeRepository(ApplicationDbContext context) : IGradeRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Result<List<Grade>>> GetCollegeGradesAsync(Guid CollegeId, CancellationToken cancellationToken = default)
    {
        var isExist = await _context.Colleges.AnyAsync(c => c.Id == CollegeId && !c.IsDeleted);
        if (!isExist)
            return Result.Failure<List<Grade>>(CollegeErrors.NotFound);

        var grades = await _context.Grades
            .Where(grade => grade.CollegeId == CollegeId && !grade.IsDeleted)
            .OrderBy(grade => grade.MinScore)
            .ToListAsync(cancellationToken);
        return Result.Success(grades);
    }

    public async Task<Result<Grade>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Grades.FirstOrDefaultAsync(grade => grade.Id == id && !grade.IsDeleted, cancellationToken)
            is Grade grade
            ? Result.Success(grade)
            : Result.Failure<Grade>(GradeErrors.NotFound);
    }
    public async Task<Result> CheckOverLabedScoresAsync(int MinScore, int MaxScore, CancellationToken cancellationToken = default)
    {
        var isExist = await _context.Grades
            .AnyAsync(g => (!(g.MinScore > MaxScore || g.MaxScore < MinScore))
            && !g.IsDeleted, cancellationToken);

        if (isExist)
            return Result.Failure(GradeErrors.InvalidScores);
        return Result.Success();
    }

    public async Task<Result> CheckOverLabedScoresAsync(int MinScore, int MaxScore, Guid Id, CancellationToken cancellationToken = default)
    {
        var isExist = await _context.Grades
           .AnyAsync(g => (!(g.MinScore > MaxScore || g.MaxScore < MinScore))
           && !g.IsDeleted
           && g.Id != Id, cancellationToken);

        if (isExist)
            return Result.Failure(GradeErrors.InvalidScores);
        return Result.Success();
    }
}

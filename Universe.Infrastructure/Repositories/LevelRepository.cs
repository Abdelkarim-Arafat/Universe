using Microsoft.EntityFrameworkCore;
using Universe.Core.Abstractions;
using Universe.Core.Entities;
using Universe.Core.Errors;
using Universe.Core.Interfaces.Repositories;
using Universe.Infrastructure.Persistence;
namespace Universe.Infrastructure.Repositories;

public class LevelRepository(ApplicationDbContext context) : ILevelRepository
{
    private readonly ApplicationDbContext context = context;

    public async Task<Result> CheckOverLabedHoursAsync(int MinHours, int MaxHours, CancellationToken cancellationToken)
    {
        var isExist = await context.Levels
            .AnyAsync(lv => (!(lv.MinHours > MaxHours || lv.MaxHours < MinHours))
            && !lv.IsDeleted, cancellationToken);
        if (isExist)
            return Result.Failure(LevelErrors.InvalidHours);
        return Result.Success();
        
    }

    public async Task<Result> CheckOverLabedHoursAsync(int MinHours, int MaxHours, Guid Id, CancellationToken cancellationToken = default)
    {
        var isExist = await context.Levels
             .AnyAsync(lv => (!(lv.MinHours > MaxHours || lv.MaxHours < MinHours))
             && lv.Id != Id
             && !lv.IsDeleted, cancellationToken);
        if (isExist)
            return Result.Failure(LevelErrors.InvalidHours);

        return Result.Success();

    }

    public async Task<Result<List<Level>>> GetCollegeLevelsAsync(Guid CollegeId, CancellationToken cancellationToken = default)
    {
        var isExist = await context.Colleges.AnyAsync(cl => cl.Id == CollegeId && !cl.IsDeleted, cancellationToken);

        if (!isExist)
            return Result.Failure<List<Level>>(CollegeErrors.NotFound);

        var levels = await context.Levels.Where(lv => lv.CollegeId == CollegeId && !lv.IsDeleted).ToListAsync(cancellationToken);
        return Result.Success(levels);

    }

    public async Task<Result<Level>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await context.Levels.FirstOrDefaultAsync(lv => lv.Id == id && !lv.IsDeleted, cancellationToken)
            is Level level
            ? Result.Success(level)
            : Result.Failure<Level>(LevelErrors.NotFound);
    }
}

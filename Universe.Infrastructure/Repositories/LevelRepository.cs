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

    public async Task<bool> CheckOverLabedHoursAsync(int MinHours, int MaxHours, CancellationToken cancellationToken)
    {
        var isExist = await context.Levels
            .AnyAsync(lv => (!(lv.MinHours > MaxHours || lv.MaxHours < MinHours))
            && !lv.IsDeleted, cancellationToken);
        return isExist;
    }

    public async Task<bool> CheckOverLabedHoursAsync(int MinHours, int MaxHours, Guid Id, CancellationToken cancellationToken = default)
    {
        var isExist = await context.Levels
             .AnyAsync(lv => (!(lv.MinHours > MaxHours || lv.MaxHours < MinHours))
             && lv.Id != Id
             && !lv.IsDeleted, cancellationToken);
        return isExist;
    }
    public async Task<Level?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await context.Levels.FirstOrDefaultAsync(lv => lv.Id == id && !lv.IsDeleted, cancellationToken);
    }
}

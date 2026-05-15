using Microsoft.EntityFrameworkCore;
using Universe.Core.Entities;
using Universe.Core.Interfaces.Repositories;
using Universe.Infrastructure.Persistence;
namespace Universe.Infrastructure.Repositories;

public class LevelRepository
    (ApplicationDbContext context) : ILevelRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<bool> CheckOverLabedHoursAsync(int MinHours, int MaxHours, Guid AcademicProgramId, CancellationToken cancellationToken)
    {
        var isExist = await _context.Levels
            .AnyAsync(lv => (!(lv.MinHours > MaxHours || lv.MaxHours < MinHours))
            && !lv.IsDeleted
            && lv.AcademicProgramId == AcademicProgramId, cancellationToken);

        return isExist;
    }
    public async Task<bool> CheckOverLabedHoursAsync(int MinHours, int MaxHours, Guid Id, Guid AcademicProgramId, CancellationToken cancellationToken = default)
    {
        var isExist = await _context.Levels
             .AnyAsync(lv => (!(lv.MinHours > MaxHours || lv.MaxHours < MinHours))
             && lv.Id != Id
             && !lv.IsDeleted
             && lv.AcademicProgramId == AcademicProgramId, cancellationToken);
        return isExist;
    }
    public async Task<Level?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Levels.FirstOrDefaultAsync(lv => lv.Id == id && !lv.IsDeleted, cancellationToken);
    }

    public async Task<Guid?> GetStudentCurrentLevelIdAsync
        (Guid programId, decimal studentTotalEarnedHours, CancellationToken cancellationToken)
    {

        return await _context.Levels.Where(
            lv => !lv.IsDeleted
            && lv.AcademicProgramId == programId
            && lv.MinHours <= studentTotalEarnedHours
            && lv.MaxHours >= studentTotalEarnedHours)
            .Select(lv => lv.Id)
            .FirstOrDefaultAsync(cancellationToken);
    }
    public async Task<bool> IsLevelExistAsync(Guid levelId, CancellationToken cancellationToken)
    {
        return await _context.Levels.AnyAsync(lv => lv.Id == levelId && !lv.IsDeleted, cancellationToken);
    }
}

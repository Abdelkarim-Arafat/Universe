using Microsoft.EntityFrameworkCore;
using Universe.Core.Entities;
using Universe.Core.Interfaces.Repositories;
using Universe.Infrastructure.Persistence;
namespace Universe.Infrastructure.Repositories;

public class LevelRepository
    (ApplicationDbContext context,
    IUserRepository userRepository) : ILevelRepository
{
    private readonly ApplicationDbContext _context = context;
    private readonly IUserRepository _userRepository = userRepository;

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

    public async Task<List<Level>> GetLevelsByProgramId(Guid AcademicProgramId, CancellationToken cancellationToken = default)
    {
        return await _context.Levels
            .AsNoTracking()
            .Where(lv => lv.AcademicProgramId == AcademicProgramId && !lv.IsDeleted)
            .ToListAsync(cancellationToken);
    }
    public async Task<Level?> GetStudentCurrentLevelAsync
    (Guid StudentId, CancellationToken cancellationToken)
    {
        int creditHours = (int)await _userRepository.CalculateCreditHoursAsync(StudentId, null, cancellationToken);
        return await _context.Levels
            .FirstOrDefaultAsync(lv => creditHours >= lv.MinHours
            && creditHours <= lv.MaxHours
            && !lv.IsDeleted, cancellationToken);
    }
}

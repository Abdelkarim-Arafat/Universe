using Microsoft.EntityFrameworkCore;
using Universe.Core.Abstractions;
using Universe.Core.Entities;
using Universe.Core.Errors;
using Universe.Core.Interfaces.Repositories;
using Universe.Infrastructure.Persistence;

namespace Universe.Infrastructure.Repositories;

public class BuildingRepository(ApplicationDbContext context) : IBuildingRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Building?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Buildings.FirstOrDefaultAsync(b => b.Id == id && !b.IsDeleted, cancellationToken);
    }
    public async Task<bool> CheckIfRoomExistAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Rooms.AnyAsync(r => r.BuildingId == id && !r.IsDeleted, cancellationToken);
    }

    public async Task<bool> IsExistAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Buildings.AnyAsync(b => b.Id == id && !b.IsDeleted, cancellationToken);
    }
}

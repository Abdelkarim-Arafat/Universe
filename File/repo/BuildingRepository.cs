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

    public async Task<Result<Building>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Buildings.FirstOrDefaultAsync(b => b.Id == id && !b.IsDeleted, cancellationToken)
            is Building building
            ? Result.Success(building)
            : Result.Failure<Building>(BuildingErrors.NotFound);
    }
    public async Task<Result<List<Building>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var buildings = await _context.Buildings.Where(b => !b.IsDeleted).ToListAsync(cancellationToken);
        return Result.Success(buildings);
    }

    public async Task<Result> CheckIfRoomExistAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var isExist = await _context.Rooms.AnyAsync(r => r.BuildingId == id && !r.IsDeleted, cancellationToken);
        if (isExist)
            return Result.Failure(BuildingErrors.RoomsFounded);
        return Result.Success();
    }

    public async Task<Result> CheckIfBuildingExistAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var isExist = await _context.Buildings.AnyAsync(b => b.Id == id && !b.IsDeleted, cancellationToken);
        if (!isExist)
            return Result.Failure(BuildingErrors.NotFound);
        return Result.Success();
    }
}

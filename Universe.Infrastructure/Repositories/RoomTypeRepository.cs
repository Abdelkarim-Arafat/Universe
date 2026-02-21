using Microsoft.EntityFrameworkCore;
using Universe.Core.Abstractions;
using Universe.Core.Entities;
using Universe.Core.Errors;
using Universe.Core.Interfaces.Repositories;
using Universe.Infrastructure.Persistence;

namespace Universe.Infrastructure.Repositories;

public class RoomTypeRepository(ApplicationDbContext context) : IRoomTypeRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<bool> CheckIfRoomTypeExist(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.RoomTypes.AnyAsync(type => type.Id == id && !type.IsDeleted, cancellationToken);
    }
    public async Task<RoomType?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.RoomTypes.FirstOrDefaultAsync(type => type.Id == id && !type.IsDeleted, cancellationToken);
    }

}

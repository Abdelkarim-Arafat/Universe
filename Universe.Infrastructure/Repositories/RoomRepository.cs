using Microsoft.EntityFrameworkCore;
using Universe.Core.Entities;
using Universe.Core.Interfaces.Repositories;
using Universe.Infrastructure.Persistence;

namespace Universe.Infrastructure.Repositories;

public class RoomRepository(ApplicationDbContext context) : IRoomRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<bool> CheckValidRoomNumberAsync(Guid BuildingId, int RoomNumber, CancellationToken cancellationToken = default)
    {
        return await _context.Rooms
            .AnyAsync(room => room.RoomNumber == RoomNumber && room.BuildingId == BuildingId && !room.IsDeleted);
    }

    public async Task<bool> CheckValidRoomNumberAsync(Guid Id, Guid BuildingId, int RoomNumber, CancellationToken cancellationToken = default)
    {
       return await _context.Rooms
             .AnyAsync(room => room.RoomNumber == RoomNumber
             && room.BuildingId == BuildingId
             && !room.IsDeleted
             && room.Id != Id);
    }

    public async Task<Room?> GetByIdAsync(Guid Id, CancellationToken cancellationToken = default)
    {
        return await _context.Rooms
            .FirstOrDefaultAsync(room => room.Id == Id && !room.IsDeleted, cancellationToken);
    }
}

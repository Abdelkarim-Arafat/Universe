using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Abstractions;
using Universe.Core.Entities;
using Universe.Core.Errors;
using Universe.Core.Interfaces.Repositories;
using Universe.Infrastructure.Persistence;

namespace Universe.Infrastructure.Repositories;

public class RoomRepository(ApplicationDbContext context) : IRoomRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Result> CheckValidRoomNumberAsync(Guid BuildingId, int RoomNumber, CancellationToken cancellationToken = default)
    {
        var isExist = await _context.Rooms
            .AnyAsync(room => room.RoomNumber == RoomNumber && room.BuildingId == BuildingId && !room.IsDeleted);
        if (isExist)
            return Result.Failure(RoomErrors.UnvalidRoomNumber);
        return Result.Success();
    }

    public async Task<Result> CheckValidRoomNumberAsync(Guid Id, Guid BuildingId, int RoomNumber, CancellationToken cancellationToken = default)
    {
        var isExist = await _context.Rooms
             .AnyAsync(room => room.RoomNumber == RoomNumber
             && room.BuildingId == BuildingId
             && !room.IsDeleted
             && room.Id != Id);
        if (isExist)
            return Result.Failure(RoomErrors.UnvalidRoomNumber);
        return Result.Success();
    }

    public async Task<Result<List<Room>>> GetAllRoomsIncludingRoomTypeAsync(CancellationToken cancellationToken = default)
    {
        var rooms = await _context.Rooms.Include(room => room.RoomType).Where(room => !room.IsDeleted).ToListAsync(cancellationToken);
        return Result.Success(rooms);
    }

    public async Task<Result<Room>> GetByIdAsync(Guid Id, CancellationToken cancellationToken = default)
    {
        return await _context.Rooms.FirstOrDefaultAsync(room => room.Id == Id && !room.IsDeleted, cancellationToken)
            is Room room
            ? Result.Success(room)
            : Result.Failure<Room>(RoomErrors.RoomNotFound);
    }

    public async Task<Result<Room>> GetRoomByIdIncludingRoomTypeAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Rooms
            .Include(room => room.RoomType)
            .FirstOrDefaultAsync(room => room.Id == id && !room.IsDeleted, cancellationToken)
            is Room room
            ? Result.Success(room)
            : Result.Failure<Room>(RoomErrors.RoomNotFound);
    }
    
   

}

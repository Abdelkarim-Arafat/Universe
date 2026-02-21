using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Abstractions;
using Universe.Core.Entities;

namespace Universe.Core.Interfaces.Repositories;

public interface IRoomRepository
{
    Task<Result<List<Room>>> GetAllRoomsIncludingRoomTypeAsync(CancellationToken cancellationToken = default);
    Task<Result<Room>> GetRoomByIdIncludingRoomTypeAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result> CheckValidRoomNumberAsync(Guid BuildingId, int RoomNumber, CancellationToken cancellationToken = default);
    Task<Result> CheckValidRoomNumberAsync(Guid Id, Guid BuildingId, int RoomNumber, CancellationToken cancellationToken = default);
    Task<Result<Room>> GetByIdAsync(Guid Id, CancellationToken cancellationToken = default);
}

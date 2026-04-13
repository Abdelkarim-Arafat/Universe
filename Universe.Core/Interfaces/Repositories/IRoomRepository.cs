using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Abstractions;
using Universe.Core.Entities;

namespace Universe.Core.Interfaces.Repositories;

public interface IRoomRepository
{
    Task<bool> CheckValidRoomNumberAsync(Guid BuildingId, int RoomNumber, CancellationToken cancellationToken = default);
    Task<bool> CheckValidRoomNumberAsync(Guid Id, Guid BuildingId, int RoomNumber, CancellationToken cancellationToken = default);
    Task<Room?> GetByIdAsync(Guid Id, CancellationToken cancellationToken = default);
}

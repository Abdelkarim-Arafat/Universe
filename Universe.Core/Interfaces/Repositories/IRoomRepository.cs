using Universe.Core.Entities;

namespace Universe.Core.Interfaces.Repositories;

public interface IRoomRepository
{
    Task<bool> CheckValidRoomNumberAsync(Guid? Id, Guid BuildingId, int RoomNumber, CancellationToken cancellationToken = default);
    Task<Room?> GetByIdAsync(Guid Id, CancellationToken cancellationToken = default);
    Task<bool> IsExistAsync(Guid Id, CancellationToken cancellationToken = default);
    Task<int> GetRoomCapacity(Guid Id, CancellationToken cancellationToken = default);
    Task<bool> IsRoomHasAnotherCommitteeAsync(Guid RoomId, Guid ExamTermId, CancellationToken cancellationToken = default);
}

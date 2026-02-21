using Universe.Core.Entities;

namespace Universe.Core.Interfaces.Repositories;

public interface IRoomTypeRepository
{
    Task<RoomType?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> CheckIfRoomTypeExist(Guid id, CancellationToken cancellationToken = default);

}

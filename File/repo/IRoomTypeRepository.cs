using Universe.Core.Abstractions;
using Universe.Core.Entities;

namespace Universe.Core.Interfaces.Repositories;

public interface IRoomTypeRepository
{
    Task<Result<RoomType>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<List<RoomType>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result> CheckIfRoomTypeExist(Guid id, CancellationToken cancellationToken = default);

}

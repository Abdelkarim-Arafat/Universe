using Universe.Core.Abstractions;
using Universe.Core.Entities;

namespace Universe.Core.Interfaces.Repositories;

public interface IBuildingRepository
{
    Task<Result<Building>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<List<Building>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result> CheckIfRoomExistAsync (Guid id, CancellationToken cancellationToken = default);
    Task<Result> CheckIfBuildingExistAsync (Guid id, CancellationToken cancellationToken = default);

}

using Universe.Core.Abstractions;
using Universe.Core.Entities;

namespace Universe.Core.Interfaces.Repositories;

public interface IBuildingRepository
{
    Task<Building?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> CheckIfRoomExistAsync (Guid id, CancellationToken cancellationToken = default);
    Task<bool> CheckIfBuildingExistAsync (Guid id, CancellationToken cancellationToken = default);
}

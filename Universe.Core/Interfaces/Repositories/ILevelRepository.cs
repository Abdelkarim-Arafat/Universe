using Universe.Core.Entities;

namespace Universe.Core.Interfaces.Repositories;

public interface ILevelRepository
{
    Task<bool> CheckOverLabedHoursAsync(int MinHours, int MaxHours, CancellationToken cancellationToken);
    Task<bool> CheckOverLabedHoursAsync(int MinHours, int MaxHours, Guid Id, CancellationToken cancellationToken = default);
    Task<Level?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}
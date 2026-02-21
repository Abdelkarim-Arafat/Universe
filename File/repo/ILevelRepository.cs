using Universe.Core.Abstractions;
using Universe.Core.Entities;

namespace Universe.Core.Interfaces.Repositories;

public interface ILevelRepository
{
    Task<Result> CheckOverLabedHoursAsync(int MinHours,int MaxHours, CancellationToken cancellationToken);
    Task<Result> CheckOverLabedHoursAsync(int MinHours, int MaxHours,Guid Id, CancellationToken cancellationToken = default);
    Task<Result<Level>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Result<List<Level>>> GetCollegeLevelsAsync(Guid CollegeId, CancellationToken cancellationToken = default);
}
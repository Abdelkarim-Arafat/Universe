using Universe.Core.Entities;

namespace Universe.Core.Interfaces.Repositories;

public interface ILevelRepository
{
    Task<bool> CheckOverLabedHoursAsync(int MinHours, int MaxHours, Guid AcademicProgramId, CancellationToken cancellationToken);
    Task<bool> CheckOverLabedHoursAsync(int MinHours, int MaxHours, Guid Id, Guid AcademicProgramId, CancellationToken cancellationToken = default);
    Task<Level?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Level>> GetLevelsByProgramId(Guid AcademicProgramId, CancellationToken cancellationToken = default);
    Task<Level?> GetStudentCurrentLevelAsync
    (Guid StudentId, CancellationToken cancellationToken);

}
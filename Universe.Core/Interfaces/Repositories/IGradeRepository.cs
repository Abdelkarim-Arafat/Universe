using Universe.Core.Abstractions;
using Universe.Core.Entities;

namespace Universe.Core.Interfaces.Repositories;

public interface IGradeRepository
{
     Task<Grade?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
     Task<List<Grade>> GetCollegeGradesAsync(Guid CollegeId, CancellationToken cancellationToken = default);
     Task<bool> CheckOverLabedScoresAsync(int MinScore, int MaxScore, CancellationToken cancellationToken = default);
     Task<bool> CheckOverLabedScoresAsync(int MinScore, int MaxScore, Guid Id, CancellationToken cancellationToken = default);
}
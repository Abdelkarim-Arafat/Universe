using Universe.Core.Abstractions;
using Universe.Core.Entities;

namespace Universe.Core.Interfaces.Repositories;

public interface IGradeRepository
{
     Task<Result<Grade>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
     Task<Result<List<Grade>>> GetCollegeGradesAsync(Guid CollegeId, CancellationToken cancellationToken = default);
     Task<Result> CheckOverLabedScoresAsync(int MinScore, int MaxScore, CancellationToken cancellationToken = default);
     Task<Result> CheckOverLabedScoresAsync(int MinScore, int MaxScore, Guid Id, CancellationToken cancellationToken = default);
}
using Universe.Core.Entities;

namespace Universe.Core.Interfaces.Repositories;

public interface IGradeRepository
{
    Task<Grade?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Grade>> GetProgramGradesAsync(Guid AcademicProgramId, CancellationToken cancellationToken = default);
    Task<bool> CheckOverLabedPointsAsync
            (decimal MinGradePoint, decimal MaxGradePoint, Guid? Id, Guid AcademicProgramId, CancellationToken cancellationToken = default);
    Task<bool> CheckOverLabedScoresAsync
            (int MinScore, int MaxScore, Guid? Id, Guid AcademicProgramId, CancellationToken cancellationToken = default);
}
using Universe.Core.Contracts.Grades;
using Universe.Core.Entities;

namespace Universe.Core.Interfaces.Repositories;

public interface IGradeRepository
{
    Task<Grade?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<GradeResponse>> GetProgramGradesAsync(Guid AcademicProgramId, CancellationToken cancellationToken = default);
    Task<bool> CheckOverLappedPointsAsync
            (decimal MinGradePoint, decimal MaxGradePoint, Guid? Id, Guid AcademicProgramId, CancellationToken cancellationToken = default);
    Task<bool> CheckOverLappedScoresAsync
            (int MinScore, int MaxScore, Guid? Id, Guid AcademicProgramId, CancellationToken cancellationToken = default);
    Task<string> GetLetterGradeByTotalDegree
        (Guid AcademicProgramId, decimal TotalDegree, CancellationToken cancellationToken = default);
}
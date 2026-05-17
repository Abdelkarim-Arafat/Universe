using Microsoft.EntityFrameworkCore;
using Universe.Core.Contracts.Level;
using Universe.Core.Entities;
using Universe.Core.Enums;
using Universe.Core.Interfaces.Repositories;
using Universe.Infrastructure.Persistence;

namespace Universe.Infrastructure.Repositories;

public class StudyLoadByLevelRepository(
    ApplicationDbContext context
    ) : IStudyLoadByLevelRepository
{
    private readonly ApplicationDbContext _context = context;
    public async Task<bool> IsExistAsync(
        Guid programId,
        Guid levelId,
        TermType semesterType,
        CancellationToken cancellationToken)
    {
        return await _context.StudyLoadByLevels
                .AnyAsync(x => x.AcademicProgramId == programId && x.LevelId == levelId && x.SemesterType == semesterType);
    }

    public async Task<StudyLoadByLevel?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => await _context.StudyLoadByLevels
        .Include(x => x.Level)
        .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<IQueryable<StudyLoadByLevel>> GetAllStudyLoadByLevelAsync(Guid programId, CancellationToken cancellationToken)
        => _context.StudyLoadByLevels
        .Include(x => x.Level)
        .Where(x => x.AcademicProgramId == programId);

    public async Task<StudentStudyLoadDto?> GetLevelStudyLoadAsync
      (Guid levelId, TermType semesterType, CancellationToken cancellationToken)
    {
        return await _context.StudyLoadByLevels
            .Where(studyLoad => studyLoad.LevelId == levelId 
                && studyLoad.SemesterType == semesterType 
                && !studyLoad.IsDeleted)
            .Select(s => new StudentStudyLoadDto
            (
                s.Level.Name,
                s.MinHours,
                s.MaxHours
            )).FirstOrDefaultAsync(cancellationToken);
    }
}

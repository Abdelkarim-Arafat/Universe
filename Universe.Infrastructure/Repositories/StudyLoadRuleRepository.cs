using Microsoft.EntityFrameworkCore;
using Universe.Core.Entities;
using Universe.Core.Interfaces.Repositories;
using Universe.Infrastructure.Persistence;

namespace Universe.Infrastructure.Repositories;

public class StudyLoadRuleRepository(ApplicationDbContext context) : IStudyLoadRuleRepository
{
    private readonly ApplicationDbContext _context = context;
    public async Task<bool> CheckOverLabedGpaAsync(Guid AcademicProgramId , Guid? Id , decimal MinGpa, decimal MaxGpa, CancellationToken cancellationToken)
        => await _context.StudyLoadRules
            .AnyAsync(x => AcademicProgramId == x.AcademicProgramId && 
            (Id == null || x.Id != Id) && 
            ((MinGpa >= x.GpaFrom && MinGpa <= x.GpaTo)
            || (MaxGpa >= x.GpaFrom && MaxGpa <= x.GpaTo)) , cancellationToken);

    public async Task<StudyLoadRule?> GetByGpaAsync(decimal Gpa, CancellationToken cancellationToken)
    {
        return await _context.StudyLoadRules
           .FirstOrDefaultAsync(x => x.GpaFrom <= Gpa && x.GpaTo >= Gpa && !x.IsDeleted, cancellationToken);
    }

    public async Task<StudyLoadRule?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await _context.StudyLoadRules.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);
}

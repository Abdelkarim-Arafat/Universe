using Mapster;
using Microsoft.EntityFrameworkCore;
using Universe.Core.Entities;
using Universe.Core.Interfaces.Repositories;
using Universe.Infrastructure.Persistence;

namespace Universe.Infrastructure.Repositories;

public class AcademicProgramRepository(ApplicationDbContext context) : IAcademicProgramRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<AcademicProgram?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => await _context.AcademicPrograms
                .SingleOrDefaultAsync(d => d.Id == id && !d.IsDeleted, cancellationToken);

    public async Task<IEnumerable<AcademicProgram>> GetAllAsync(Guid CollegeId, CancellationToken cancellationToken)
        => await _context.AcademicPrograms
                .AsNoTracking()
                .Where(d => d.CollegeId == CollegeId && !d.IsDeleted).ToListAsync(cancellationToken);
    public async Task<bool> IsExistAsync(Guid AcademicProgramId, string Name, string Code, Guid? excludeId, CancellationToken cancellationToken)
    => await _context.AcademicPrograms
        .AnyAsync(d => d.Id == AcademicProgramId &&
                       !d.IsDeleted &&
                       (excludeId == null || d.Id != excludeId)
        && (d.Name == Name || d.Code == Code), cancellationToken);
    public async Task<bool> IsExistAsync(Guid AcademicProgramId, CancellationToken cancellationToken)
    => await _context.AcademicPrograms
        .AnyAsync(d => d.Id == AcademicProgramId &&
                       !d.IsDeleted, cancellationToken);


    public async Task<ProgramSchedule?> GetScheduleAsync(Guid ProgramId , Guid SemesterId , CancellationToken cancellationToken)
        => await _context.ProgramSchedules
        .Where(x => x.ProgramId == ProgramId && x.SemesterId == SemesterId)
        .SingleOrDefaultAsync(cancellationToken);

    public async Task<Guid?> GetCurrentAcademicProgramIdAsync(Guid StudentId, CancellationToken cancellationToken)
    {
        return await _context.StudentAcademicPrograms
            .Where(pro => pro.StudentId == StudentId
                   && pro.Currently
                   && !pro.IsDeleted)
            .Select(pro => pro.AcademicProgramId)
            .FirstOrDefaultAsync(cancellationToken);
    }
}

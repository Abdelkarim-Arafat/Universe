using Microsoft.EntityFrameworkCore;
using Universe.Core.Entities;
using Universe.Core.Interfaces.Repositories;
using Universe.Infrastructure.Persistence;

namespace Universe.Infrastructure.Repositories;

public class AcademicEventRepository(ApplicationDbContext context) : IAcademicEventRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<bool> IsOverlabedAsync(
    Guid programId,
    Guid semesterId,
    Core.Enums.EventType eventType,
    DateOnly startDate,
    DateOnly endDate,
    CancellationToken cancellationToken)
    {
        return await _context.AcademicEvents
            .AnyAsync(e =>
                e.Type == eventType &&
                e.ProgramId == programId &&
                e.SemesterId == semesterId &&
                startDate < e.EndDate &&
                endDate > e.StartDate,
                cancellationToken);
    }

    public async Task<AcademicEvent?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => await _context.AcademicEvents
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

    public async Task<AcademicEvent?> GetByProgramAndSemesterIdsAsync(Guid programId,
        Guid semesterId,
        Core.Enums.EventType eventType,
        CancellationToken cancellationToken)
    {
        return await _context.AcademicEvents
            .FirstOrDefaultAsync(e =>
                e.Type == eventType &&
                e.ProgramId == programId &&
                e.SemesterId == semesterId,
                cancellationToken);
    }
}

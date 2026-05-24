using Universe.Core.Entities;

namespace Universe.Core.Interfaces.Repositories;

public interface IAcademicEventRepository
{
    Task<bool> IsOverlabedAsync(Guid programId,
        Guid semesterId,
        Core.Enums.EventType eventType,
        DateOnly startDate,
        DateOnly endDate,
        CancellationToken cancellationToken);

    Task<AcademicEvent?> GetByProgramAndSemesterIdsAsync(Guid programId,
        Guid semesterId,
        Core.Enums.EventType eventType,
        CancellationToken cancellationToken);

    Task<AcademicEvent?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}

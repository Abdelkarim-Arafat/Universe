using Microsoft.Data.SqlClient.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities;

namespace Universe.Core.Interfaces.Repositories;

public interface IAcademicEventRepository
{
    Task<bool> IsOverlabedAsync(Guid programId,
        Guid semesterId,
        Core.Enums.EventType eventType,
        DateTime startDate,
        DateTime endDate,
        CancellationToken cancellationToken);

    Task<AcademicEvent?> GetByProgramAndSemesterIdsAsync(Guid programId,
        Guid semesterId,
        Core.Enums.EventType eventType,
        CancellationToken cancellationToken);
}

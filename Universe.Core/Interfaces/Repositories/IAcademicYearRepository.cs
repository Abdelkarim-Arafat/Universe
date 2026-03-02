using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities;

namespace Universe.Core.Interfaces.Repositories;

public interface IAcademicYearRepository
{
    Task<AcademicYear?> GetByIdAsync(Guid Id, CancellationToken cancellationToken);
    Task<bool> IsMakeConflictAsync(Guid CollegeId, string Name, DateOnly start, DateOnly end, Guid? Id, CancellationToken cancellationToken);
}

using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities;

namespace Universe.Core.Interfaces.Repositories;

public interface IAcademicProgramRepository 
{
    Task<bool> IsExistAsync(Guid AcademicProgramId, string name, string code, Guid? excludeId, CancellationToken cancellationToken);
    Task<AcademicProgram?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<AcademicProgram>> GetAllAsync(Guid AcademicProgramId, CancellationToken cancellationToken);
}

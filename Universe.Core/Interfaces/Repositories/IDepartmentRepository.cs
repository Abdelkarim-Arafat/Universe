using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities;

namespace Universe.Core.Interfaces.Repositories;

public interface IDepartmentRepository 
{
    Task<bool> IsExistAsync(Guid collegeId, string name, string code, Guid? excludeId, CancellationToken cancellationToken);
    Task<Department?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Department>> GetAllAsync(Guid CollegeId, CancellationToken cancellationToken);
}

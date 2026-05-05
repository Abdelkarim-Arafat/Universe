using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Core.Interfaces.Repositories;

public interface ICollegeRepository
{
    Task<bool> IsExistAsync(Guid Id, CancellationToken cancellationToken = default);
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Core.Interfaces.Repositories;

public interface ICollegeRepository
{
    Task<bool> CheckCollegeIsExistAsync(Guid Id, CancellationToken cancellationToken = default);
}

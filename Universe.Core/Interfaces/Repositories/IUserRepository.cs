using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities;

namespace Universe.Core.Interfaces.Repositories;

public interface IUserRepository
{
    Task<RefreshToken?> GetRefreshTokenAsync(string token , CancellationToken cancellationToken);
}

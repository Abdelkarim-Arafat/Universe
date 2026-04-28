using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities;

namespace Universe.Core.Interfaces.Repositories;

public interface IServiceRepository
{
    Task<Service?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<ServiceRequest?> GetRequestByIdAsync(Guid id, CancellationToken cancellationToken);
}

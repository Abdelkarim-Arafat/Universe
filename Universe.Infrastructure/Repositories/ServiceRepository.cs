using Microsoft.EntityFrameworkCore;
using Universe.Core.Entities;
using Universe.Core.Interfaces.Repositories;
using Universe.Infrastructure.Persistence;

namespace Universe.Infrastructure.Repositories;

public class ServiceRepository(ApplicationDbContext context) : IServiceRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Service?> GetByIdAsync(Guid id , CancellationToken cancellationToken)
        => await _context.Services.FirstOrDefaultAsync(x =>  x.Id == id , cancellationToken);

    public async Task<ServiceRequest?> GetRequestByIdAsync(Guid id , CancellationToken cancellationToken)
        => await _context.ServiceRequests.FirstOrDefaultAsync(x => x.Id == id , cancellationToken);
}

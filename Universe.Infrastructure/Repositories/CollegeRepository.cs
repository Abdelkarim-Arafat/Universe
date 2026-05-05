using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Interfaces.Repositories;
using Universe.Infrastructure.Persistence;

namespace Universe.Infrastructure.Repositories;

public class CollegeRepository(ApplicationDbContext context) : ICollegeRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<bool> IsExistAsync(Guid Id, CancellationToken cancellationToken = default)
    {
       return await _context.Colleges.AnyAsync(c => c.Id == Id && !c.IsDeleted, cancellationToken);
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities;
using Universe.Core.Interfaces.Repositories;
using Universe.Infrastructure.Persistence;

namespace Universe.Infrastructure.Repositories;

public class AcademicProgramRepository(ApplicationDbContext context) : IAcademicProgramRepository
{
    private readonly ApplicationDbContext _context = context;


    public async Task<AcademicProgram?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => await _context.AcademicPrograms
                .SingleOrDefaultAsync(d => d.Id == id && !d.IsDeleted, cancellationToken);

    public async Task<IEnumerable<AcademicProgram>> GetAllAsync(Guid CollegeId, CancellationToken cancellationToken)
        => await _context.AcademicPrograms
                .AsNoTracking()
                .Where(d => d.CollegeId == CollegeId && !d.IsDeleted).ToListAsync(cancellationToken);
    public async Task<bool> IsExistAsync(Guid collegeId, string name, string code, Guid? excludeId, CancellationToken cancellationToken)
    => await _context.AcademicPrograms
        .AnyAsync(d => d.CollegeId == collegeId &&
                       !d.IsDeleted &&
                       (d.Name == name || d.Code == code) &&
                       (excludeId == null || d.Id != excludeId) , cancellationToken);
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities;
using Universe.Core.Interfaces.Repositories;
using Universe.Infrastructure.Persistence;

namespace Universe.Infrastructure.Repositories;

internal class AcademicYearRepository(ApplicationDbContext context) : IAcademicYearRepository
{
    private readonly ApplicationDbContext _context = context;
    public async Task<AcademicYear?> GetByIdAsync(Guid Id , CancellationToken cancellationToken)
        => await _context.AcademicYears.FirstOrDefaultAsync(x => x.Id == Id , cancellationToken);

    public async Task<bool> IsSemesterExistAsync(Guid Id, CancellationToken cancellationToken)
        => await _context.Semesters.AnyAsync(x => x.Id == Id , cancellationToken);

    public async Task<bool> IsMakeConflictAsync(Guid CollegeId, string Name, DateOnly start, DateOnly end, Guid? Id, CancellationToken cancellationToken)
        => await _context.AcademicYears
            .AnyAsync(x => (Id == null || x.Id != Id)
            && CollegeId == x.CollegeId
            && Name == x.Name
            && ((start >= x.StartDate && start <= x.EndDate)
            || (end >= x.StartDate && end <= x.EndDate)) , cancellationToken);

    //public async Task<Semester> GetSemesterByTypeAsync()
}

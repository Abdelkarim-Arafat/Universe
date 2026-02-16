using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using Universe.Core.Entities;
using Universe.Core.Interfaces.Repositories;
using Universe.Infrastructure.Persistence;

namespace Universe.Infrastructure.Repositories;

public class CourseRepository(ApplicationDbContext context) : ICourseRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Course?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => await _context.Courses
                .SingleOrDefaultAsync(d => d.Id == id && !d.IsDeleted, cancellationToken);

    public async Task<IEnumerable<Course>> GetAllAsync(Guid collegeId, CancellationToken cancellationToken)
        => await _context.Courses
                .Where(d => d.CollegeId == collegeId && !d.IsDeleted).ToListAsync(cancellationToken);
    public async Task<bool> IsExistAsync(Guid collegeId, string name, string code, Guid? excludeId, CancellationToken cancellationToken)
    => await _context.Courses
        .AnyAsync(d => d.CollegeId == collegeId &&
                       !d.IsDeleted &&
                       (d.Name == name || d.Code == code) &&
                       (excludeId == null || d.Id != excludeId), cancellationToken);

    public async Task<CoursePrerequisite?> GetCoursePreRequisiteAsync(Guid courseId , Guid PreRequisiteId , CancellationToken cancellationToken)
        => await _context.CoursePrerequisites
            .SingleOrDefaultAsync(d => d.CourseId == courseId && d.PrerequisiteCourseId == PreRequisiteId, cancellationToken);

    public async Task<IEnumerable<Course>> GetAllPreRequisiteAsync(Guid courseId , CancellationToken cancellationToken)
        => await _context.CoursePrerequisites
            .Where(c => c.CourseId == courseId)
            .Select(c => c.PrerequisiteCourse)
            .ToListAsync(cancellationToken);
    public async Task<bool> IsExistCoursePreRequisiteAsync(Guid courseId , Guid preRequisiteId , CancellationToken cancellationToken)
        => await _context.CoursePrerequisites
            .AnyAsync(d => d.CourseId == courseId && d.PrerequisiteCourseId == preRequisiteId, cancellationToken);
}

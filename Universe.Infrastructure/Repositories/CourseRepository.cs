using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using Universe.Core.Contracts.Course;
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

    public async Task<CourseWithPreRequisiteResponse?> GetCourseWithPrerequisitesAsync(
    Guid id,
    CancellationToken cancellationToken)
    {
        return await _context.Courses
            .Where(x => x.Id == id)
            .Select(x => new CourseWithPreRequisiteResponse(
                x.Id.ToString(),
                x.Name,
                x.Description,
                x.Code,
                x.RequirementType,
                x.Prerequisites
                    .Select(p => new CourseResponse(
                        p.PrerequisiteCourse.Id.ToString(),
                        p.PrerequisiteCourse.Name,
                        p.PrerequisiteCourse.Code
                    )).ToList()
            ))
            .FirstOrDefaultAsync(cancellationToken);
    }

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

    public async Task<IList<Guid>> ExistingPreRequisitesIdsAsync(List<Guid> preRequisitesIds , CancellationToken cancellationToken)
        => await _context.Courses
            .Where(c => preRequisitesIds.Contains(c.Id) && !c.IsDeleted)
            .Select(c => c.Id)
            .ToListAsync(cancellationToken);

    public async Task<IList<Guid>> GetDirectPreRequisitesIdsAsync(Guid courseId, CancellationToken cancellationToken)
        => await _context.CoursePrerequisites
            .Where(c => c.CourseId == courseId)
            .Select(c => c.PrerequisiteCourseId)
            .ToListAsync(cancellationToken);

    public async Task RemovePrerequisiteAsync(
    Guid courseId,
    Guid preReqId,
    CancellationToken cancellationToken)
    {
        var entity = await _context.CoursePrerequisites
            .FirstOrDefaultAsync(x =>
                x.CourseId == courseId &&
                x.PrerequisiteCourseId == preReqId,
                cancellationToken);

        if (entity != null)
            _context.CoursePrerequisites.Remove(entity);
    }
}

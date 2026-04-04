using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities;
using Universe.Core.Interfaces.Repositories;
using Universe.Infrastructure.Persistence;

namespace Universe.Infrastructure.Repositories;

public class CourseOfferingRepository(ApplicationDbContext context) : ICourseOfferingRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<bool> IsExistAsync(Guid AcademicProgramId, Guid SemesterId,
        Guid LevelId, Guid CourseId, CancellationToken cancellationToken)
            => await _context.CourseOfferings.AnyAsync(c => !c.IsDeleted
                && c.AcademicProgramId == AcademicProgramId
                && c.SemesterId == SemesterId
                && c.LevelId == LevelId
                && c.CourseId == CourseId, cancellationToken);


    public Task<CourseOffering?> GetByIdAsync(Guid Id, CancellationToken cancellationToken)
        => _context.CourseOfferings
        .FirstOrDefaultAsync(c => c.Id == Id && !c.IsDeleted, cancellationToken);

    public async Task<List<CourseOfferingAssessment>> GetCourseOfferingAssessments(Guid CourseOfferingId , CancellationToken cancellationToken)
        => await _context.CourseOfferingAssessments
        .Where(c => c.CourseOfferingId == CourseOfferingId && !c.IsDeleted)
        .ToListAsync(cancellationToken);

    public async Task<List<CourseOffering>> GetCourseOfferingsByLevelAndSemesterIncludingCourseAsync(Guid LevelId, Guid SemesterId, CancellationToken cancellationToken)
    {
        return await _context.CourseOfferings
            .AsNoTracking()
            .Include(c => c.Course) 
            .Where(c=>c.LevelId == LevelId && c.SemesterId == SemesterId&& !c.IsDeleted)
            .ToListAsync(cancellationToken);
    }

    public async Task<Dictionary<Guid, List<CourseOfferingAssessment>>>
        GetCourseOfferingsAssessmentsBulkAsync(
            List<Guid> courseOfferingIds,
            CancellationToken cancellationToken)
    {

        var data = await _context.CourseOfferingAssessments
            .Where(a => courseOfferingIds.Contains(a.CourseOfferingId) && !a.IsDeleted)
            .ToListAsync(cancellationToken);


        var dict = data
            .GroupBy(a => a.CourseOfferingId)
            .ToDictionary(
                g => g.Key,
                g => g.ToList()
            );

        return dict;
    }


}

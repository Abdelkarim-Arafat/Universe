using Microsoft.EntityFrameworkCore;
using Universe.Core.Entities;
using Universe.Core.Enums;
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

    public async Task<bool> IsExistAsync(Guid CourseOfferingId, CancellationToken cancellationToken)
    {
        return await _context.CourseOfferings.AnyAsync(co => co.Id == CourseOfferingId, cancellationToken);
    }
    public Task<CourseOffering?> GetByIdAsync(Guid Id, CancellationToken cancellationToken)
        => _context.CourseOfferings
        .FirstOrDefaultAsync(c => c.Id == Id && !c.IsDeleted, cancellationToken);

    public async Task<List<CourseOfferingAssessment>> GetCourseOfferingAssessments(Guid CourseOfferingId, CancellationToken cancellationToken)
        => await _context.CourseOfferingAssessments
        .Where(c => c.CourseOfferingId == CourseOfferingId && !c.IsDeleted)
        .ToListAsync(cancellationToken);

    public async Task<List<CourseOffering>> GetAvailableCourseOfferingsAsync(
        Guid levelId,
        Guid semesterId,
        Guid studentId,
        CancellationToken cancellationToken)
    {
        return await _context.CourseOfferings
            .AsNoTracking()
            .Include(c => c.Course)
            .Where(c => c.LevelId == levelId
                     && c.SemesterId == semesterId
                     && !c.IsDeleted

                     && !c.Enrollments.Any(e => e.StudentId == studentId
                                             && !e.IsDeleted
                                             && e.Status == EnrollmentStatus.Passed))
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
    public async Task<int> CountCourseAssessments(List<Guid> CourseAssessmentsIds, CancellationToken cancellationToken)
    {
        return await _context.CourseOfferingAssessments
            .CountAsync(a => CourseAssessmentsIds.Contains(a.Id) && !a.IsDeleted, cancellationToken);
    }

    public async Task<decimal> RegistredHours(List<Guid> CourseOfferingIds, CancellationToken cancellationToken)
    {
        return await _context.CourseOfferings
            .Where(co => CourseOfferingIds.Contains(co.Id) && !co.IsDeleted)
            .SumAsync(co => co.CreditHours, cancellationToken);
    }
    public async Task<Dictionary<Guid, Guid>> CourseOfferingIdsToCourseIdAsync(List<Guid> CourseOfferingsIds, CancellationToken cancellationToken)
    {
        return await _context.CourseOfferings
              .Where(co => CourseOfferingsIds.Contains(co.Id) && !co.IsDeleted)
              .ToDictionaryAsync(co => co.Id, co => co.CourseId, cancellationToken);
    }
    public async Task<bool> IsOpenForControlAsync(Guid courseOfferingId, CancellationToken cancellationToken)
    {
        return await _context.CourseOfferings
            .Where(co => co.Id == courseOfferingId && !co.IsDeleted)
            .Select(co => co.IsOpenForControl)
            .FirstOrDefaultAsync(cancellationToken);
    }
    public async Task<CourseOffering?> GetByIdIncludingEnrollmentsAsync(Guid courseOfferingId, CancellationToken cancellationToken)
    {
        return await _context.CourseOfferings
            .Where(co => co.Id == courseOfferingId && !co.IsDeleted)
            .Include(co => co.Enrollments)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
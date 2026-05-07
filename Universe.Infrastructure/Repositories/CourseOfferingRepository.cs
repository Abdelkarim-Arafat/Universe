using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using Universe.Core.Contracts.CourseOffering;
using Universe.Core.Contracts.TeachingSession;
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

    public async Task<IReadOnlyList<SessionResponse>> GetCourseOfferingSessionsAsync(Guid courseOfferingId , int GroupNumber, CancellationToken cancellationToken)
        => await _context.CourseOfferingSessions
            .AsNoTracking()
            .Where(x => x.CourseOfferingId == courseOfferingId
                && x.TeachingSession.GroupNumber == GroupNumber)
            .Select(x => new SessionResponse(
                x.TeachingSession.Id,
                x.TeachingSession.StartTime,
                x.TeachingSession.EndTime,
                x.TeachingSession.Type,
                x.TeachingSession.Day,
                x.TeachingSession.InstructorId,
                x.TeachingSession.Instructor.Name,
                x.TeachingSession.RoomId,
                x.TeachingSession.Room.Name,
                x.TeachingSession.GroupNumber
            ))
            .ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<CourseOfferingResponse>> GetLevelCoursesAsync(Guid LevelId, Guid SemesterId, CancellationToken cancellationToken)
        => await _context.CourseOfferings
            .AsNoTracking()
            .Where(c => c.LevelId == LevelId && c.SemesterId == SemesterId && !c.IsDeleted)
            .Select(c => new CourseOfferingResponse(
                c.Id,
                c.Course.Name,
                c.Course.Code,
                c.NumberOfGroups
            ))
            .ToListAsync(cancellationToken);

    public async Task<bool> IsExistAsync(Guid CourseOfferingId, CancellationToken cancellationToken)
        => await _context.CourseOfferings
            .AnyAsync(co => co.Id == CourseOfferingId && !co.IsDeleted, cancellationToken);
    public Task<CourseOffering?> GetByIdAsync(Guid Id, CancellationToken cancellationToken)
        => _context.CourseOfferings
        .FirstOrDefaultAsync(c => c.Id == Id && !c.IsDeleted, cancellationToken);

    public async Task<CourseOffering?> GetByIdIncludingAssessmentsAsync(Guid Id, CancellationToken cancellationToken)
        => await _context.CourseOfferings
            .Include(co => co.Assessments)
            .FirstOrDefaultAsync(c => c.Id == Id && !c.IsDeleted, cancellationToken);
    public async Task<List<CourseOfferingAssessment>> GetCourseOfferingAssessments(Guid CourseOfferingId, CancellationToken cancellationToken)
        => await _context.CourseOfferingAssessments
        .Where(c => c.CourseOfferingId == CourseOfferingId && !c.IsDeleted)
        .ToListAsync(cancellationToken);

    public async Task<List<CourseOffering>> GetAvailableCourseOfferingsIncludingCourseAsync(
        Guid levelId,
        Guid semesterId,
        Guid studentId,
        CancellationToken cancellationToken)
    {

        var passedCourses = _context.Enrollments
              .Where(e => e.StudentId == studentId
               && e.Status == EnrollmentStatus.Passed
               && !e.IsDeleted)
              .Select(e => e.CourseOffering.CourseId);

        return await _context.CourseOfferings
            .AsNoTracking()
            .Include(offer => offer.Course)
            .Where(c => c.LevelId == levelId
                     && c.SemesterId == semesterId
                     && !c.IsDeleted
                     && !passedCourses.Contains(c.CourseId)
                     && !_context.CoursePrerequisites
                           .Where(p => p.CourseId == c.CourseId)
                           .Any(p => !passedCourses.Contains(p.PrerequisiteCourseId)))
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

    public async Task<Guid?> GetIdByCourseAssessmentIdAsync(Guid CourseAssessmentId, CancellationToken cancellationToken)
    {
      return await _context.CourseOfferingAssessments
            .Where(coa => coa.Id == CourseAssessmentId && !coa.IsDeleted)
            .Select(coa => coa.CourseOfferingId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<int> NumberOfRegisteredStudentsAsync(Guid CourseOfferingId, CancellationToken cancellationToken)
    {
        return await _context.Enrollments
             .CountAsync(enroll => !enroll.IsDeleted && enroll.CourseOfferingId == CourseOfferingId);
    }
}
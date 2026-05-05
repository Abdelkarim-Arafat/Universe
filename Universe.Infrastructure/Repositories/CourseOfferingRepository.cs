using Microsoft.EntityFrameworkCore;
using Universe.Application.EnrollmentServices.Dtos;
using Universe.Core.Dtos.Enrollments;
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

    public async Task<decimal> RegistredHours(List<Guid> CourseOfferingIds, CancellationToken cancellationToken)
    {
        return await _context.CourseOfferings
            .Where(co => CourseOfferingIds.Contains(co.Id) && !co.IsDeleted)
            .SumAsync(co => co.CreditHours, cancellationToken);
    }

    public async Task<LevelRegistrationCatalogDto?> GetAvailableCoursesCatalogAsync(
     Guid studentId,
     Guid semesterId,
     Guid levelId,
     CancellationToken cancellationToken)
    {
        var passedCoursesIds = _context.Enrollments
            .Where(e => e.StudentId == studentId && e.Status == EnrollmentStatus.Passed && !e.IsDeleted)
            .Select(e => e.CourseOffering.CourseId);

        return await _context.Levels
            .AsNoTracking()
            .Where(l => l.Id == levelId && !l.IsDeleted)
            .Select(l => new LevelRegistrationCatalogDto(
                l.Name, 

                _context.CourseOfferings
                    .Where(co => co.LevelId == levelId
                              && co.SemesterId == semesterId
                              && !co.IsDeleted
                              && !passedCoursesIds.Contains(co.CourseId)
                              && !_context.CoursePrerequisites
                                    .Where(p => p.CourseId == co.CourseId)
                                    .Any(p => !passedCoursesIds.Contains(p.PrerequisiteCourseId)))
                    .Select(co => new CourseRegistration(
                        co.Id,
                        co.CourseId,
                        co.Course.Name,
                        co.Course.Code,
                        co.IsOptional,
                        co.CreditHours,
                        false, // IsEnrolled
                        co.CourseOfferingSessions
                            .Where(cos => !cos.IsDeleted)
                            .Select(cos => new SessionInfo(
                                cos.TeachingSessionId,
                                cos.TeachingSession.Instructor.Name,
                                cos.TeachingSession.Type,
                                cos.TeachingSession.GroupNumber,
                                cos.TeachingSession.Day,
                                cos.TeachingSession.StartTime,
                                cos.TeachingSession.EndTime,
                                cos.TeachingSession.Capacity 
                                - cos.TeachingSession.TeachingSessionEnrollments.Count(e => !e.IsDeleted),
                                false // IsRegistered
                            )).ToList()
                    )).ToList()
            ))
            .FirstOrDefaultAsync(cancellationToken);
    }
}
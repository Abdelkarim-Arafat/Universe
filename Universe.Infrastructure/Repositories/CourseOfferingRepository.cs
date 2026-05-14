using Microsoft.EntityFrameworkCore;
using Universe.Core.Contracts.CourseOffering;
using Universe.Core.Contracts.Enrollments;
using Universe.Core.Contracts.TeachingSession;
using Universe.Core.Entities;
using Universe.Core.Enums;
using Universe.Core.Interfaces.Repositories;
using Universe.Infrastructure.Persistence;
using System.Linq.Dynamic.Core;
using Universe.Core.Contracts.Student;

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

    public async Task<IReadOnlyList<SessionResponse>> GetCourseOfferingSessionsAsync(
        Guid courseOfferingId,
        int GroupNumber,
        CancellationToken cancellationToken)
        => await _context.CourseOfferingSessions
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
    public async Task<CourseOffering?> GetByIdAsync(Guid Id, CancellationToken cancellationToken)
        => await _context.CourseOfferings
                      .FirstOrDefaultAsync(c => c.Id == Id && !c.IsDeleted, cancellationToken);

    public Task<List<Guid>> GetStudentsIdsEnrolledInCourseAsync
        (Guid courseOfferingId, CancellationToken cancellationToken)
    {
        return _context.Enrollments
            .Where(e => e.CourseOfferingId == courseOfferingId && !e.IsDeleted)
            .Select(e => e.StudentId)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<CourseOfferingAssessment>> GetCourseOfferingAssessments
        (Guid CourseOfferingId, CancellationToken cancellationToken)
        => await _context.CourseOfferingAssessments
        .Where(c => c.CourseOfferingId == CourseOfferingId && !c.IsDeleted)
        .ToListAsync(cancellationToken);

    public async Task<List<CourseOfferingAssessmentResponse>> GetCourseOfferingAssessmentsForViewAsync
        (Guid CourseOfferingId, CancellationToken cancellationToken)
       => await _context.CourseOfferingAssessments
       .Where(c => c.CourseOfferingId == CourseOfferingId && !c.IsDeleted)
       .Select(c => new CourseOfferingAssessmentResponse(
           c.Id,
           c.Type,
           c.MaxScore
       ))
       .ToListAsync(cancellationToken);


    // راجع
    public async Task<List<CourseRegistrationData>> GetAvailableCoursesForRegistrationAsync(
     Guid studentId,
     Guid semesterId,
     Guid levelId,
     CancellationToken cancellationToken)
    {
        var passedCoursesIds = _context.Enrollments
            .Where(e => e.StudentId == studentId && e.Status == EnrollmentStatus.Passed && !e.IsDeleted)
            .Select(e => e.CourseOffering.CourseId);

        return await _context.CourseOfferings
                    .Where(co =>  co.LevelId == levelId
                              &&  co.SemesterId == semesterId
                              && !co.IsDeleted
                              && !passedCoursesIds.Contains(co.CourseId)
                              &&  co.Course.Prerequisites
                                    .All(p => passedCoursesIds.Contains(p.PrerequisiteCourseId)))
                    .Select(co => new CourseRegistrationData(
                        co.Id,
                        co.CourseId,
                        co.Course.Name,
                        co.Course.Code,
                        co.IsOptional,
                        co.CreditHours,
                        co.Enrollments.Any(enrollment => !enrollment.IsDeleted
                                               && enrollment.StudentId == studentId
                                               && enrollment.Status == EnrollmentStatus.InProgress),
                        co.CourseOfferingSessions
                            .Where(cos => !cos.IsDeleted)
                            .Select(cos => new SessionInfo (
                                cos.TeachingSessionId,
                                cos.TeachingSession.Instructor.Name,
                                cos.TeachingSession.Type,
                                cos.TeachingSession.GroupNumber,
                                cos.TeachingSession.Day,
                                cos.TeachingSession.StartTime,
                                cos.TeachingSession.EndTime,
                                cos.TeachingSession.Capacity
                                - cos.TeachingSession.TeachingSessionEnrollments.Count(e => !e.IsDeleted),
                                (cos.TeachingSession.TeachingSessionEnrollments.Any(tse =>
                                tse.Enrollment.StudentId == studentId && !tse.IsDeleted))
                            )).ToList()
                    )).ToListAsync(cancellationToken);
    }

    public async Task<CourseOffering?> GetByIdIncludingAssessmentsAsync(Guid Id, CancellationToken cancellationToken)
    {
        return await _context.CourseOfferings
            .Include(co => co.Assessments)
            .FirstOrDefaultAsync(co => !co.IsDeleted && co.Id == Id, cancellationToken);
    }
    public async Task<CourseOfferingData?> GetCourseOfferingDataByAssessmentIdAsync
        (Guid courseOfferingAssessmentId, CancellationToken cancellationToken)
    {
        return await _context.CourseOfferingAssessments
            .Where(co => co.Id == courseOfferingAssessmentId && !co.IsDeleted)
            .Select(co => new CourseOfferingData(
                co.CourseOffering.IsOpenForControl,
                co.CourseOffering.SuccessPercentage,
                co.CourseOfferingId
            ))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<decimal> CalculateCreditHoursForCoursesAsync
        (List<Guid> courseOfferingsIds, CancellationToken cancellationToken)
    {
        return await _context.CourseOfferings
            .Where(c => courseOfferingsIds.Contains(c.Id) && !c.IsDeleted)
            .SumAsync(c => c.CreditHours, cancellationToken);
    }

    public async Task<ILookup<Guid, Guid>> GetAssessmentIdsGroupedByOfferingAsync(
    List<Guid> incomingCourseOfferingIds,
    CancellationToken cancellationToken)
    {
        var assessments = await _context.CourseOfferingAssessments
            .Where(a => incomingCourseOfferingIds.Contains(a.CourseOfferingId) && !a.IsDeleted)
            .Select(a => new { a.CourseOfferingId, a.Id })
            .ToListAsync(cancellationToken);

        return assessments.ToLookup(a => a.CourseOfferingId, a => a.Id);
    }
}
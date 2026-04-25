using Microsoft.EntityFrameworkCore;
using Universe.Core.Entities;
using Universe.Core.Interfaces.Repositories;
using Universe.Infrastructure.Persistence;

namespace Universe.Infrastructure.Repositories;

public class EnrollmentRepository(
    ApplicationDbContext context
    ) : IEnrollmentRepository
{  
    private readonly ApplicationDbContext _context = context;
    
    public async Task<List<Enrollment>> GetStudentEnrollmentsWithSessions(Guid StudentId, CancellationToken cancellationToken)
    {
        return await _context.Enrollments
            .Include(e => e.TeachingSessionEnrollments)
            .Where(e => e.StudentId == StudentId && !e.IsDeleted
             && e.Status == Core.Enums.EnrollmentStatus.InProgress)
            .ToListAsync(cancellationToken);
    }

    public async Task<Dictionary<Guid, int>> GetOccupiedSeatsBulkAsync(List<Guid> SessionsIds, CancellationToken cancellationToken)
    {
        return await _context.TeachingSessions
            .Where(s => SessionsIds.Contains(s.Id) && !s.IsDeleted)
            .Select(s => new
            {
                SessionId = s.Id,
                Count = s.TeachingSessionEnrollments.Count(tse => !tse.IsDeleted)
            })
            .ToDictionaryAsync(x => x.SessionId, x => x.Count, cancellationToken);
    }

    public async Task<List<(TeachingSession Session, int EnrolledCount)>> GetSessionsWithAvailabilityBulk(
     Guid courseOfferingId,
     CancellationToken cancellationToken)
    {
        var results = await _context.CourseOfferingSessions
            .AsNoTracking()
            .Where(x => x.CourseOfferingId == courseOfferingId)
            .Select(x => new
            {
                
                Session = x.TeachingSession,
                Instructor = x.TeachingSession.Instructor,
                EnrolledCount = x.TeachingSession.TeachingSessionEnrollments.Count()
            })
            .ToListAsync(cancellationToken);

       
        return results.Select(r =>
        {    
            r.Session.Instructor = r.Instructor;
            return (r.Session, r.EnrolledCount);
        }).ToList();
    }

    public async Task<List<TeachingSessionEnrollment>> GetTeachingSessionEnrollmentAsync(Guid StudentId, CancellationToken cancellationToken)
    {
        return await _context.TeachingSessionEnrollments
    .AsNoTracking()
    .Include(x => x.Enrollment)
    .Include(x => x.TeachingSession)
    .Where(x => !x.IsDeleted
             && x.Enrollment.StudentId == StudentId
             && !x.Enrollment.IsDeleted
             && x.Enrollment.Status == Core.Enums.EnrollmentStatus.InProgress)
    .ToListAsync(cancellationToken);
    }
    // لازم تضيف dtos
    public async Task<List<Enrollment>> GetStudentEnrollmentsAsync(Guid studentId, CancellationToken cancellationToken)
    {
        return await _context.Enrollments
        .AsNoTracking()
          .Where(e => e.StudentId == studentId && !e.IsDeleted)
          .Include(e => e.CourseOffering)
            .ThenInclude(co => co.Course)
          .Include(e => e.CourseOffering.Semester)
            .ThenInclude(s => s.AcademicYear)  
            .AsSplitQuery()  
            .ToListAsync(cancellationToken);
    }   
    public async Task<decimal> CalculateRegistredHoursAsync(Guid studentId, CancellationToken cancellationToken)
    {
        return await _context.Enrollments
            .Where(e => e.StudentId == studentId && !e.IsDeleted
             && e.Status == Core.Enums.EnrollmentStatus.InProgress)
            .SumAsync(e => e.CourseOffering.CreditHours, cancellationToken);
    }

    public async Task<Enrollment?> GetEnrollmentByStudentIdAndCourseOfferingIdAsync(Guid StudentId, Guid CourseOfferingId, CancellationToken cancellationToken)
    {
        return await _context.Enrollments
            .Include(e => e.CourseOffering)
            .FirstOrDefaultAsync(e => e.StudentId == StudentId
            && e.CourseOfferingId == CourseOfferingId, cancellationToken);
    }
}

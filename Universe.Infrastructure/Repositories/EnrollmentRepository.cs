using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities;
using Universe.Core.Interfaces.Repositories;
using Universe.Infrastructure.Persistence;

namespace Universe.Infrastructure.Repositories;

public class EnrollmentRepository(
    ApplicationDbContext context
    ) : IEnrollmentRepository
{  
    private readonly ApplicationDbContext _context = context;

    public Task<Enrollment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _context.Enrollments.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);
    }

    public async Task<bool> IsStudentPassedInCourse(Guid StudentId, Guid CourseId, CancellationToken cancellationToken)
    {
        return await _context.Enrollments
            .AnyAsync
            (x => x.StudentId == StudentId
             && x.CourseId == CourseId
             && x.Status == Core.Enums.EnrollmentStatus.Passed
             && !x.IsDeleted);
    }
     
    public async Task<int> NumberOfEnrollmentsInSession(Guid TeachingSessionId, CancellationToken cancellationToken)
    {
        return await _context.TeachingSessionEnrollments
             .CountAsync(t => t.TeachingSessionId == TeachingSessionId, cancellationToken);
    }
    public async Task<int> AvailableSeatsInSession(Guid TeachingSessionId, CancellationToken cancellationToken)
    {
        int capacity = await _context.TeachingSessions
            .Where(ts => ts.Id == TeachingSessionId)
            .Select(ts => ts.Capacity)
            .FirstOrDefaultAsync(cancellationToken);
        int enrolledCount = await NumberOfEnrollmentsInSession(TeachingSessionId, cancellationToken);
        return capacity - enrolledCount;
    }

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
       return await _context.TeachingSessionEnrollments
            .Where(tse => SessionsIds.Contains(tse.TeachingSessionId) && !tse.IsDeleted)
            .GroupBy(tse => tse.TeachingSessionId)
            .Select(g => new { SessionId = g.Key, Count = g.Count() })
            .ToDictionaryAsync(x => x.SessionId, x => x.Count, cancellationToken);
    }

    public async Task<List<TeachingSessionEnrollment>> GetTeachingSessionEnrollmentAsync(Guid StudentId, CancellationToken cancellationToken)
    {
        var EnrollmentsIds = await _context.Enrollments
            .Where(e => e.StudentId == StudentId && !e.IsDeleted
             && e.Status == Core.Enums.EnrollmentStatus.InProgress)
            .Select(e => e.Id)
            .ToListAsync(cancellationToken);
        return await _context.TeachingSessionEnrollments
            .AsNoTracking()
            .Include(x => x.Enrollment)
            .Include(x => x.TeachingSession)
            .Where(x => EnrollmentsIds.Contains(x.EnrollmentId) && !x.IsDeleted)
            .ToListAsync(cancellationToken);
    }
}

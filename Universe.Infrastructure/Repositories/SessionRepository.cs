using Microsoft.EntityFrameworkCore;
using Universe.Core.Entities;
using Universe.Core.Enums;
using Universe.Core.Interfaces.Repositories;
using Universe.Infrastructure.Persistence;

namespace Universe.Infrastructure.Repositories;

public class SessionRepository(ApplicationDbContext context) : ISessionRepository
{
    private readonly ApplicationDbContext _context = context;
    public async Task<CourseOfferingSession?> GetCourseOfferingSessionByIdAsync(
        Guid courseId,
        Guid sessionId,
        CancellationToken cancellationToken)
    {
        return await _context.CourseOfferingSessions
            .FirstOrDefaultAsync(x => x.CourseOfferingId == courseId &&
                x.TeachingSessionId == sessionId,
                cancellationToken);
    }

    public async Task<TeachingSession?> GetMatchingSessionAsync(
    Guid courseId,
    TimeOnly startTime,
    TimeOnly endTime,
    Core.Enums.DayOfWeek day,
    SessionType type,
    Guid instructorId,
    Guid roomId,
    int groupNumber,
    CancellationToken cancellationToken)
    {
        return await _context.TeachingSessions
            .Where(s =>
                s.StartTime == startTime &&
                s.EndTime == endTime &&
                s.Day == day &&
                s.Type == type &&
                s.InstructorId == instructorId &&
                s.RoomId == roomId &&
                s.GroupNumber == groupNumber
            )
            .Where(s => s.CourseOfferingSessions
                .Any(cs => cs.CourseOffering.CourseId == courseId))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<TeachingSession>> GetCourseOfferingGroupSessionsAsync(
        Guid courseId,
        CancellationToken cancellationToken)
    {
        return await _context.CourseOfferingSessions
            .AsNoTracking()
            .Where(x => x.CourseOfferingId == courseId)
            .Select(x => x.TeachingSession)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> HasSessionsAsync(Guid programId, Guid semesterId, CancellationToken cancellationToken)
        => await _context.CourseOfferingSessions
            .AnyAsync(x => x.CourseOffering.AcademicProgramId == programId
                        && x.CourseOffering.SemesterId == semesterId);

    public async Task<TeachingSession?> GetByIdAsync(Guid sessionId , CancellationToken cancellationToken)
        => await _context.TeachingSessions.FirstOrDefaultAsync(x => x.Id == sessionId , cancellationToken);

    public async Task<bool> HasOtherCoursesAsync(Guid sessionId, Guid courseId, CancellationToken cancellationToken)
        => await _context.CourseOfferingSessions
        .AnyAsync(x => x.TeachingSessionId == sessionId &&
            x.CourseOfferingId != courseId,
            cancellationToken);

    public async Task<bool> IsUsedTimeAsync(
    Guid courseOfferingId,
    TimeOnly start,
    TimeOnly end,
    Core.Enums.DayOfWeek day,
    int groupNumber,
    CancellationToken cancellationToken)
    {
        return await _context.TeachingSessions
           .AsNoTracking()
           .AnyAsync(x =>
               x.GroupNumber == groupNumber &&
               x.Day == day &&
               ((start >= x.StartTime && start < x.EndTime) ||
               (end > x.StartTime && end < x.EndTime)) &&
               x.CourseOfferingSessions.Any(cs =>
                   cs.CourseOfferingId == courseOfferingId),
               cancellationToken);
    }

    public async Task<bool> IsInstructorBusyAsync(
    Guid id,
    Guid semesterId,
    TimeOnly start,
    TimeOnly end,
    Core.Enums.DayOfWeek day,
    CancellationToken cancellationToken)
    {
        return await _context.TeachingSessions
           .AsNoTracking()
           .AnyAsync(x =>
               x.InstructorId == id &&
               x.Day == day &&
               ((start >= x.StartTime && start <= x.EndTime) ||
               (end >= x.StartTime && end <= x.EndTime)) &&
               ((start >= x.StartTime && start < x.EndTime) ||
               (end > x.StartTime && end < x.EndTime)) && 
               x.CourseOfferingSessions.Any(cs =>
                   cs.CourseOffering.SemesterId == semesterId &&
                   !cs.CourseOffering.IsDeleted),
               cancellationToken);
    }

    public async Task<bool> IsRoomBusyAsync(
    Guid id,
    Guid semesterId,
    TimeOnly start,
    TimeOnly end,
    Core.Enums.DayOfWeek day,
    CancellationToken cancellationToken)
    {
        return await _context.TeachingSessions
           .AsNoTracking()
           .AnyAsync(x =>
               x.RoomId == id &&
               x.Day == day &&
               ((start >= x.StartTime && start < x.EndTime) ||
               (end > x.StartTime && end < x.EndTime)) &&
               x.CourseOfferingSessions.Any(cs =>
                   cs.CourseOffering.SemesterId == semesterId &&
                   !cs.CourseOffering.IsDeleted),
               cancellationToken);
    }

    public async Task<List<TeachingSession>> GetSessionsByIdIncludingCourseOfferingAsync(IEnumerable<Guid> SessionsIds, CancellationToken cancellationToken)
    {
        return await _context.TeachingSessions
           .AsNoTracking()
           .Include(session => session.CourseOfferingSessions)
           .Where(x => SessionsIds.Contains(x.Id))
           .ToListAsync();
    }

    public async Task<List<TeachingSession>> GetSessionsByCourseOfferingIncludingInstructor(Guid CourseOfferingId, CancellationToken cancellationToken)
    {
        var SessionsIds = await _context.CourseOfferingSessions
            .AsNoTracking()
            .Where(x => x.CourseOfferingId == CourseOfferingId)
            .Select(x => x.TeachingSessionId)
            .ToListAsync(cancellationToken);

        return await _context.TeachingSessions
            .AsNoTracking()
            .Include(t => t.Instructor)
            .Where(x => SessionsIds.Contains(x.Id))
            .ToListAsync(cancellationToken);
    }
     
    public async Task<int> GetGroupNumberAsync(Guid SessionId, CancellationToken cancellationToken)
    {
        return await _context.TeachingSessions
                    .Where(ts => ts.Id == SessionId)
                    .Select(ts => ts.GroupNumber)
                    .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Dictionary<Guid, (int GroupNumber, int Capacity)>>
        GetGroupNumberAndCapacityBulkAsync(List<Guid> sessionIds, CancellationToken cancellationToken)
    {
        return await _context.TeachingSessions
            .Where(ts => sessionIds.Contains(ts.Id))
            .ToDictionaryAsync(
                ts => ts.Id,                           
                ts => (ts.GroupNumber, ts.Capacity),    
                cancellationToken
            );
    }
    public async Task<List<CourseOfferingSession>>
        GetSessionsWithCourseOfferingIdAsync
        (List<(Guid SessionId, Guid CourseOfferingId)> newSessions, CancellationToken cancellationToken)
    {
        var sessionIds = newSessions.Select(x => x.SessionId).ToList();
        var offeringIds = newSessions.Select(x => x.CourseOfferingId).ToList();
 
        var currentSessions = await _context.CourseOfferingSessions
            .Include(s => s.TeachingSession)
            .Where(cos => sessionIds.Contains(cos.TeachingSessionId)
                       && offeringIds.Contains(cos.CourseOfferingId))
            .ToListAsync(cancellationToken);

        return currentSessions
            .Where(cos => newSessions
            .Any(pair => pair.SessionId == cos.TeachingSessionId
             && pair.CourseOfferingId == cos.CourseOfferingId))
            .ToList();
    }

    public Task<IEnumerable<TeachingSession>> GetCourseOfferingSessionsAsync(Guid courseId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

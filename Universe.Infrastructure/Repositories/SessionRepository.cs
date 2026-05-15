using Microsoft.EntityFrameworkCore;
using Universe.Core.Contracts.TeachingSession;
using Universe.Core.Contracts.Enrollments;
using Universe.Core.Entities;
using Universe.Core.Enums;
using Universe.Core.Interfaces.Repositories;
using Universe.Infrastructure.Persistence;

namespace Universe.Infrastructure.Repositories;

public class SessionRepository(ApplicationDbContext context) : ISessionRepository
{
    private readonly ApplicationDbContext _context = context;
    public async Task<IReadOnlyList<SessionResponse>> GetInstructorSessionsAsync(
     Guid programId,
     Guid instructorId,
     Guid semesterId,
     CancellationToken cancellationToken)
    {
        return await _context.TeachingSessions
            .AsNoTracking()
            .Where(x =>
                x.InstructorId == instructorId &&
                x.CourseOfferingSessions.Any(cos =>
                    cos.CourseOffering.AcademicProgramId == programId &&
                    cos.CourseOffering.SemesterId == semesterId
                )
            )
            .Select(x => new SessionResponse(
                x.Id,
                x.StartTime,
                x.EndTime,
                x.Type,
                x.Day,
                x.InstructorId,
                x.Instructor.Name,
                x.RoomId,
                x.Room.Name,
                x.GroupNumber
            ))
            .ToListAsync(cancellationToken);
    }
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

    public async Task<bool> HasSessionsAsync(Guid programId, Guid semesterId, CancellationToken cancellationToken)
        => await _context.CourseOfferingSessions
            .AnyAsync(x => x.CourseOffering.AcademicProgramId == programId
                        && x.CourseOffering.SemesterId == semesterId);

    public async Task<TeachingSession?> GetByIdAsync(Guid sessionId, CancellationToken cancellationToken)
        => await _context.TeachingSessions.FirstOrDefaultAsync(x => x.Id == sessionId, cancellationToken);

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

    public async Task<List<SessionDetailsDto>> GetIncomingSessionsDetailsAsync(
     List<Guid> sessionIds,
     CancellationToken cancellationToken)
    {
        return await _context.CourseOfferingSessions
                       .Where(cos => sessionIds.Contains(cos.TeachingSessionId) && !cos.IsDeleted)
                       .Select(cos => new SessionDetailsDto
                       (
                           cos.TeachingSessionId,
                           cos.CourseOfferingId,
                           cos.TeachingSession.Day,
                           cos.TeachingSession.StartTime,
                           cos.TeachingSession.EndTime,
                           cos.TeachingSession.Type,
                           cos.TeachingSession.GroupNumber,
                           cos.TeachingSession.Capacity,
                           cos.TeachingSession.TeachingSessionEnrollments.Count(ts => !ts.IsDeleted)
                       )).ToListAsync(cancellationToken);
    }

}

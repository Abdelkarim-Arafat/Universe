using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities;
using Universe.Core.Interfaces.Repositories;
using Universe.Infrastructure.Persistence;

namespace Universe.Infrastructure.Repositories;

public class SessionRepository(ApplicationDbContext context) : ISessionRepository
{
    private readonly ApplicationDbContext _context = context;
    public async Task<CourseOfferingSession?> GetCourseOfferingSessionByIdAsync(
        Guid courseId ,
        Guid sessionId,
        CancellationToken cancellationToken)
    {
        return await _context.CourseOfferingSessions
            .FirstOrDefaultAsync(x => x.CourseOfferingId == courseId &&
                x.TeachingSessionId == sessionId,
                cancellationToken);
    }

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
               ((start >= x.StartTime && start <= x.EndTime) ||
               (end >= x.StartTime && end <= x.EndTime)) &&
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
               ((start >= x.StartTime && start <= x.EndTime) ||
               (end >= x.StartTime && end <= x.EndTime)) &&
               x.CourseOfferingSessions.Any(cs =>
                   cs.CourseOffering.SemesterId == semesterId &&
                   !cs.CourseOffering.IsDeleted),
               cancellationToken);
    }
}

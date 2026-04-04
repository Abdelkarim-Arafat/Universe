using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities;
using Universe.Core.Enums;

namespace Universe.Core.Interfaces.Repositories;

public interface ISessionRepository
{

    Task<CourseOfferingSession?> GetCourseOfferingSessionByIdAsync(
        Guid courseId,
        Guid sessionId,
        CancellationToken cancellationToken);

    Task<TeachingSession?> GetMatchingSessionAsync(
    Guid courseId,
    TimeOnly startTime,
    TimeOnly endTime,
    Core.Enums.DayOfWeek day,
    SessionType type,
    Guid instructorId,
    Guid roomId,
    int groupNumber,
    CancellationToken cancellationToken);

    Task<bool> HasSessionsAsync(Guid programId, Guid semesterId, CancellationToken cancellationToken);

    Task<IEnumerable<TeachingSession>> GetCourseOfferingSessionsAsync(
        Guid courseId,
        CancellationToken cancellationToken);
    Task<TeachingSession?> GetByIdAsync(Guid sessionId, CancellationToken cancellationToken);

    Task<bool> HasOtherCoursesAsync(Guid sessionId, Guid courseId, CancellationToken cancellationToken);

    Task<bool> IsUsedTimeAsync(
    Guid courseOfferingId,
    TimeOnly start,
    TimeOnly end,
    Core.Enums.DayOfWeek day,
    int groupNumber,
    CancellationToken cancellationToken);


    Task<bool> IsInstructorBusyAsync(
    Guid id,
    Guid semesterId,
    TimeOnly start,
    TimeOnly end,
    Enums.DayOfWeek day,
    CancellationToken cancellationToken);

    Task<bool> IsRoomBusyAsync(
    Guid id,
    Guid semesterId,
    TimeOnly start,
    TimeOnly end,
    Enums.DayOfWeek day,
    CancellationToken cancellationToken);
    Task<List<TeachingSession>> GetSessionsByIdIncludingCourseOfferingAsync
        (IEnumerable<Guid> SessionsIds, CancellationToken cancellationToken);

    Task<List<TeachingSession>> GetSessionsByCourseOfferingIncludingInstructor
        (Guid CourseOfferingId, CancellationToken cancellationToken);
    Task<int> GetGroupNumberAsync(Guid SessionId, CancellationToken cancellationToken);
    Task<Dictionary<Guid, (int GroupNumber, int Capacity)>>
         GetGroupNumberAndCapacityBulkAsync(List<Guid> sessionIds, CancellationToken cancellationToken);
}

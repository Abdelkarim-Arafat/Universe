using Universe.Core.Contracts.TeachingSession;
using Universe.Core.Entities;
using Universe.Core.Enums;

namespace Universe.Core.Interfaces.Repositories;

public interface ISessionRepository
{

    Task<CourseOfferingSession?> GetCourseOfferingSessionByIdAsync(
        Guid courseId,
        Guid sessionId,
        CancellationToken cancellationToken);

    Task<IReadOnlyList<SessionResponse>> GetInstructorSessionsAsync(
     Guid programId,
     Guid instructorId,
     Guid semesterId,
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
}

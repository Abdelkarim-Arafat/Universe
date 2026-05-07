
using Universe.Core.Enums;

namespace Universe.Core.Contracts.TeachingSession;

public record SessionResponse(
    Guid Id,
    TimeOnly StartTime,
    TimeOnly EndTime,
    SessionType Type,
    Core.Enums.DayOfWeek Day,
    Guid InstructorId,
    string InstructorName,
    Guid RoomId,
    string RoomName,
    int GroupNumber
);  
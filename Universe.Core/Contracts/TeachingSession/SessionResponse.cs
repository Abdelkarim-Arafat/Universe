
using Universe.Core.Enums;

namespace Universe.Core.Contracts.TeachingSession;

public record SessionResponse(
    string Id,
    TimeOnly StartTime,
    TimeOnly EndTime,
    SessionType Type,
    Core.Enums.DayOfWeek Day,
    string InstructorId,
    string InstructorName,
    string RoomId,
    string RoomName,
    int GroupNumber
);  
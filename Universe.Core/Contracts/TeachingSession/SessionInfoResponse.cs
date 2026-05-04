using Universe.Core.Enums;

namespace Universe.Core.Contracts.TeachingSession;

public record SessionInfoResponse(
    Guid SessionId,
    string InstructorName,
    SessionType Type,
    int GroupNumber,
    Core.Enums.DayOfWeek Day,
    TimeOnly Start,
    TimeOnly End,
    int AvailableSeats
    //bool IsRegistered
);

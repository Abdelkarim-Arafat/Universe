using Universe.Core.Enums;

namespace Universe.Core.Contracts.Enrollments;


public record CourseRegistrationData(
    Guid CourseOfferingId,
    Guid CourseId,
    string CourseName,
    string CourseCode,
    bool IsOptional,
    decimal CreditHours,
    bool IsEnrolled,
    List<SessionInfo> Sessions
);

public record SessionInfo(
    Guid SessionId,
    string InstructorName,
    SessionType Type,
    int GroupNumber,
    Enums.DayOfWeek Day,
    TimeOnly Start,
    TimeOnly End,
    int AvailableSeats,
    bool IsRegistered
);

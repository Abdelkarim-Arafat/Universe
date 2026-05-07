using Universe.Core.Enums;

namespace Universe.Core.Contracts.Enrollments;


public record LevelRegistrationCatalogDto(
    string LevelName,
    List<CourseRegistration> Courses
);

public record CourseRegistration(
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

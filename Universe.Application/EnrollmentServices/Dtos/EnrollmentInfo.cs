using Universe.Core.Enums;

namespace Universe.Application.EnrollmentServices.Dtos;

public record EnrollmentInfo
(
    Guid EnrollemntId,
    Guid SessionId,
    Guid CourseOfferingId,
    Guid CourseId,
    SessionType Type,
    TimeOnly StartTime,
    TimeOnly EndTime,
    Core.Enums.DayOfWeek DayOfWeek
);

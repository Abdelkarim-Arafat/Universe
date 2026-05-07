using Mapster.Utils;
using Universe.Core.Enums;

namespace Universe.Application.EnrollmentServices.Dtos;

public record EnrollmentInfo
(
    Guid SessionId,
    Guid CourseOfferingId,
    string CourseName,
    string InstructorName,
    string BuildingName,
    int RoomNumber,
    int GroupNumber,
    SessionType Type,
    TimeOnly StartTime,
    TimeOnly EndTime,
    Core.Enums.DayOfWeek DayOfWeek
);

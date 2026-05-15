using Universe.Core.Enums;

namespace Universe.Core.Contracts.Enrollments;

public record StudentExistingEnrollment(
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
    Enums.DayOfWeek DayOfWeek
);


using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Enums;

namespace Universe.Core.Dtos.Enrollments;

public record EnrollmentValidationContextDto(
    string StudentName,
    string StudentCode,
    string? StudentLevelName,
    Guid? AcademicProgramId,
    bool IsSemesterValid,
    decimal? MinHours,
    decimal? MaxHours,
    decimal CurrentRegisteredHours,
    List<StudentExistingEnrollment> ExistingEnrollments 
);
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


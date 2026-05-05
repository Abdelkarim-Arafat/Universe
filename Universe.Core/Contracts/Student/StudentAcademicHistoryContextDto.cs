namespace Universe.Core.Contracts.Student;

public record StudentAcademicHistoryContextDto(
    Guid? CurrentAcademicProgramId,
    List<StudentSemesterRecord> Semesters
);

public record StudentSemesterRecord(
    string SemesterName,
    string AcademicYearName,
    DateOnly SemesterStartDate,  
    List<CourseRecord> Courses
);

public record CourseRecord(
    Guid CourseOfferingId,
    string CourseCode,
    string CourseName,
    decimal CreditHours,
    decimal TotalGrade,
    bool IsPassed
);

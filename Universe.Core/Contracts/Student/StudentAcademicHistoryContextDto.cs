namespace Universe.Core.Contracts.Student;

public record StudentAcademicHistoryContextDto(
    List<StudentSemesterRecord> Semesters
);

public record StudentSemesterRecord(
    Guid  SemesterId,
    string SemesterName,
    string AcademicYearName,
    DateOnly SemesterStartDate,  
    List<CourseDetailsDto> Courses
);

public record CourseDetailsDto(
    string CourseCode,
    string CourseName,
    decimal CreditHours,
    decimal TotalDegree,
    string LetterGrade,
    bool IsPassed
);

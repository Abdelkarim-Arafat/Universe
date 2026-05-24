namespace Universe.Core.Contracts.Student;

public record StudentAcademicHistoryDto(
    IEnumerable<StudentSemesterRecord> Semesters
);

public record StudentSemesterRecord(
    Guid SemesterId,
    string SemesterName,
    string AcademicYearName,
    DateOnly SemesterStartDate,
    IEnumerable<CourseDetailsDto> Courses
);

public record CourseDetailsDto(
    Guid CourseOfferingId,
    string CourseCode,
    string CourseName,
    decimal CreditHours,
    decimal TotalDegree,
    string LetterGrade,
    bool IsPassed
);

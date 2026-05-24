namespace Universe.Core.Contracts.Student;

public record StudentSemesterDataResponse
(
   string SemesterName,
   string AcademicYear,
   decimal SemesterGPA,
   decimal CumulativeGPA,
   decimal AttemptedHours,
   decimal EarnedHours,
   string SemesterGrade,
   string CumulativeGrade,
   List<CourseDetailsDto> Courses
);

using Universe.Core.Contracts.Student;

namespace Universe.Core.Contracts.User;

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

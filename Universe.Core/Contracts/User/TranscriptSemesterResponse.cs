
namespace Universe.Core.Contracts.User;
public record TranscriptSemesterResponse
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
public record CourseDetailsDto
(
     string CourseCode,
     string CourseName,
     decimal CreditHours,
     string LetterGrade,
     decimal? FinalGrade
);

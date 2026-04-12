namespace Universe.Application.UserServices.UserDtos;

public record TranscriptSemesterResponse
(
   string SemesterName,
   string AcademicYear,
   decimal SemesterGPA ,
   decimal CumulativeGPA ,
   int AttemptedHours ,
   int EarnedHours ,
   string SemesterGrade,
   string CumulativeGrade,
   List<CourseGradeDto> Courses 
);
public record CourseGradeDto
(
     string CourseCode,
     string CourseName,
     decimal CreditHours,
     string LetterGrade,
     decimal? FinalGrade
);

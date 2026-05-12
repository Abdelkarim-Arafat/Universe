using Universe.Core.Contracts.Student;

namespace Universe.Application.UserServices.UserDtos;
 
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

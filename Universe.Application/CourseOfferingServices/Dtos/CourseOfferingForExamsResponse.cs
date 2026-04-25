namespace Universe.Application.CourseOfferingServices.Dtos;

public record CourseOfferingForExamsResponse
(
 Guid Id,
 string CouresName,
 string CouresCode,
 int NumberOfStudents
);

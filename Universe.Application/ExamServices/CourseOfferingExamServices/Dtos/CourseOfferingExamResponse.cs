namespace Universe.Application.CourseOfferingExamServices.Dtos;

public record CourseOfferingExamResponse
(
     Guid Id,
     Guid CourseOfferingId,
     Guid ExamTermId,
     DateOnly Date,
     TimeOnly StartTime,
     TimeOnly EndTime
);
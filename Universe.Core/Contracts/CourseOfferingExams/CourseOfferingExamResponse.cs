namespace Universe.Core.Contracts.CourseOfferingExams;

public record CourseOfferingExamResponse
(
     Guid Id,
     DateOnly Date,
     TimeOnly StartTime,
     TimeOnly EndTime
);

namespace Universe.Core.Contracts.CourseOfferings;

public record CourseOfferingForExamsResponse
(
    Guid Id,
    Guid? CourseOfferingExamId,
    string CouresName,
    string CouresCode,
    int NumberOfStudents
);

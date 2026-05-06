
namespace Universe.Core.Contracts.CourseOffering;

public record CourseOfferingForExamsResponse (
    Guid Id,
    string CouresName,
    string CouresCode,
    int NumberOfStudents
);

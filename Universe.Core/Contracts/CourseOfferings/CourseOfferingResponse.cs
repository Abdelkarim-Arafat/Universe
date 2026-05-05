
namespace Universe.Core.Contracts.CourseOfferings;

public record CourseOfferingResponse(
    string Id,
    string Name,
    string Code,
    int NumberOfGroups
);

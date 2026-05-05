

using Universe.Core.Enums;

namespace Universe.Core.Contracts.Course;


public record CourseWithPreRequisiteResponse(
    string Id,
    string Name,
    string Description,
    string Code,
    RequirementType? RequirementType,
    List<CourseResponse> PreRequisites
);

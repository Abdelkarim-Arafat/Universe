using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.Course;
using Universe.Core.Enums;

namespace Universe.Application.CourseServices.Commands.AddCourse;

public record AddCourseCommand(
    [Required] Guid CollegeId,
    string Name,
    string Description,
    string Code,
    RequirementType? RequirementType,
    List<Guid> PreRequisiteIds
) : IRequest<Result<CourseWithPreRequisiteResponse>>;

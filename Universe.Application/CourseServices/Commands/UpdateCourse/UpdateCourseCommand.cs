using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Universe.Core.Contracts.Course;
using Universe.Core.Enums;

namespace Universe.Application.CourseServices.Commands.UpdateCourse;

public record UpdateCourseCommand (
    [Required] Guid Id,
    [Required] Guid CollegeId,
    string Name,
    string Description,
    string Code,
    RequirementType? RequirementType,
    List<Guid> PreRequisiteIds
) : IRequest<Result<CourseWithPreRequisiteResponse>>;

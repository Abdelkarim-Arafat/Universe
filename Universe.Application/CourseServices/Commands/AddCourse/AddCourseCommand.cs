using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.CourseServices.Dtos;

namespace Universe.Application.CourseServices.Commands.AddCourse;

public record AddCourseCommand(
    [Required] Guid CollegeId,
    string Name,
    string Description,
    string Code,
    List<Guid> PreRequisiteIds
) : IRequest<Result<CourseResponse>>;

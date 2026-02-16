using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.CourseServices.Dtos;

namespace Universe.Application.CourseServices.Query.GetCourse;

public record GetCourseCommand(
    [Required] Guid Id
) : IRequest<Result<CourseResponse>>;

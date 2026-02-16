using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.CourseServices.Dtos;

namespace Universe.Application.CourseServices.Query.GetCoursePrerequisite;

public record GetCoursePreRequisiteCommand(
    [Required] Guid Id
) : IRequest<Result<List<CourseResponse>>>;
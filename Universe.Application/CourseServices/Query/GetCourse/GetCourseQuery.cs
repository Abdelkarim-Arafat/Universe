using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.Course;

namespace Universe.Application.CourseServices.Query.GetCourse;

public record GetCourseQuery(
    [Required] Guid CollegeId,
    [Required] Guid Id
) : IRequest<Result<CourseWithPreRequisiteResponse>>;

using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.CourseServices.Dtos;

namespace Universe.Application.CourseServices.Query.GetAllCourses;

public record GetAllCoursesCommand(
    [Required] Guid CollegeId
) : IRequest<Result<List<CourseResponse>>>;

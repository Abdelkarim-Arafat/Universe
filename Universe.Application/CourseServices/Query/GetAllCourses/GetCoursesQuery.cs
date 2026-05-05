using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.Course;

namespace Universe.Application.CourseServices.Query.GetAllCourses;

public record GetCoursesQuery (
    [Required] Guid CollegeId ,
    FilterRequest filter
) : IRequest<Result<PaginationList<CourseResponse>>>;
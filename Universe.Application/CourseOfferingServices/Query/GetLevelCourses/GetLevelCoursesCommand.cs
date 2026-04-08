using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.CourseOfferingServices.Dtos;
using Universe.Core.Enums;

namespace Universe.Application.CourseOfferingServices.Query.GetLevelCourses;

public record GetLevelCoursesCommand(
    [Required] Guid LevelId,
    [Required] Guid SemesterId
) : IRequest<Result<List<CourseOfferingResponse>>>;

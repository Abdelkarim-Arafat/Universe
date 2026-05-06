using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.CourseOffering;
using Universe.Core.Enums;

namespace Universe.Application.CourseOfferingServices.Query.GetLevelCourses;

public record GetLevelCoursesQuery(
    [Required] Guid AcademicProgramId,
    [Required] Guid LevelId,
    [Required] Guid AcademicYearId,
    [Required] TermType SemesterType
) : IRequest<Result<IReadOnlyList<CourseOfferingResponse>>>;

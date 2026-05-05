
using Universe.Core.Contracts.CourseOfferings;
using Universe.Core.Enums;

namespace Universe.Application.CourseOfferingServices.Query.GetLevelCourses;

public record GetLevelCoursesCommand(
    [Required] Guid LevelId,
    [Required] Guid AcademicYearId,
    [Required] TermType SemesterType
) : IRequest<Result<List<CourseOfferingResponse>>>;

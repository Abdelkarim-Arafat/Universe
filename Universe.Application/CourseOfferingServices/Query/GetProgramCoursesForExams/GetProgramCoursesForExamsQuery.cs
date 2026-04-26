using Universe.Application.CourseOfferingServices.Dtos;

namespace Universe.Application.CourseOfferingServices.Query.GetProgramCoursesForExams;

public record GetProgramCoursesForExamsQuery
    // add semester
([Required] Guid AcademicProgramId,
 [Required] Guid SemesterId,
    FilterRequest Filter)
    : IRequest<Result<PaginationList<CourseOfferingForExamsResponse>>>;
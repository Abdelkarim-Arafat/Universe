using Universe.Application.CourseOfferingServices.Dtos;

namespace Universe.Application.CourseOfferingServices.Query.GetProgramCoursesForExams;

public record GetProgramCoursesForExamsQuery
([Required] Guid AcademicProgramId,
    FilterRequest Filter)
    : IRequest<Result<PaginationList<CourseOfferingForExamsResponse>>>;
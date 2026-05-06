using Universe.Core.Contracts.CourseOffering;


namespace Universe.Application.CourseOfferingServices.Query.GetProgramCoursesForExams;

public record GetProgramCoursesForExamsQuery (
    [Required] Guid AcademicProgramId,
    [Required] Guid SemesterId,
    FilterRequest Filter
) : IRequest<Result<PaginationList<CourseOfferingForExamsResponse>>>;
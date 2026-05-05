using Universe.Core.Contracts.CourseOfferings;

namespace Universe.Application.CourseOfferingServices.Query.GetProgramCoursesForExams;

public record GetProgramCoursesForExamsQuery
(
  [Required] Guid AcademicProgramId,
  [Required] Guid SemesterId,
  [Required] Guid examTermId,
  FilterRequest Filter
) : IRequest<Result<PaginationList<CourseOfferingForExamsResponse>>>;
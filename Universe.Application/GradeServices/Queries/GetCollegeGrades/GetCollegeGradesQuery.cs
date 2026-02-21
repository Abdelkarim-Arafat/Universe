using Universe.Application.GradeServices.GradeDtos;

namespace Universe.Application.GradeServices.Queries.GetCollegeGrades;

public record GetCollegeGradesQuery
(
   [Required] Guid CollegeId,
    FilterRequest Filter
) : IRequest<Result<PaginationList<GradeResponse>>>;

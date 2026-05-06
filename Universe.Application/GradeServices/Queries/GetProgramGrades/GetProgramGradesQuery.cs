namespace Universe.Application.GradeServices.Queries.GetProgramGrades;

public record GetProgramGradesQuery
(
   [Required] Guid AcademicProgramId,
    FilterRequest Filter
) : IRequest<Result<PaginationList<GradeResponse>>>;

namespace Universe.Application.GradeServices.Queries.GetGradesByProgram;

public record GetGradesByProgramQuery
(
   [Required] Guid AcademicProgramId,
    FilterRequest Filter
) : IRequest<Result<PaginationList<GradeResponse>>>;

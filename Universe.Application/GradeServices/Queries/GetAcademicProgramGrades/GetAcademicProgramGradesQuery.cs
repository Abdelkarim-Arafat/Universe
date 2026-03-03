namespace Universe.Application.GradeServices.Queries.GetAcademicProgramGrades;

public record GetAcademicProgramGradesQuery
(
   [Required] Guid AcademicProgramId,
    FilterRequest Filter
) : IRequest<Result<PaginationList<GradeResponse>>>;

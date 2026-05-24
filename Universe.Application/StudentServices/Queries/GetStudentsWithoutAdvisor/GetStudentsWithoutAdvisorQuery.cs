namespace Universe.Application.StudentServices.Queries.GetStudentsWithoutAdvisor;

public record GetStudentsWithoutAdvisorQuery(
    [Required] Guid ProgramId,
    FilterRequest Filter
) : IRequest<Result<PaginationList<StudentResponse>>>;
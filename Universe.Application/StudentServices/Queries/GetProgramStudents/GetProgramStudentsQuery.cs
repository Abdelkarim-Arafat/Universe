namespace Universe.Application.StudentServices.Queries.GetProgramStudents;

public record GetProgramStudentsQuery(
    [Required] Guid ProgramId,
    FilterRequest Filter
) : IRequest<Result<PaginationList<StudentResponse>>>;
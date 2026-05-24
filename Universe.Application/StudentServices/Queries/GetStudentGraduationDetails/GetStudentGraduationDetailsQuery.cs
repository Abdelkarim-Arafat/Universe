namespace Universe.Application.StudentServices.Queries.GetStudentGraduationDetails;

public record GetStudentGraduationDetailsQuery(
    [Required] Guid StudentId,
    [Required] Guid ProgramId
) : IRequest<Result<GraduationDetailsResponse>>;
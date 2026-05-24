namespace Universe.Application.StudentServices.Queries.GetStudentExams;

public record GetStudentExamsQuery(
    [Required] Guid StudentId
) : IRequest<Result<StudentExamsResponse>>;


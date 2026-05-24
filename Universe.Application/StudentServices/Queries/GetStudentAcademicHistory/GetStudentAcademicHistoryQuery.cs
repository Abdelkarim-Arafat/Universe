namespace Universe.Application.StudentServices.Queries.GetStudentAcademicHistory;

public record GetStudentAcademicHistoryQuery
(
    [Required] Guid StudentId
) : IRequest<Result<List<StudentSemesterDataResponse>>>;
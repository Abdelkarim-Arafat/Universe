using Universe.Core.Contracts.User;

namespace Universe.Application.UserServices.Querys.GetStudentAcademicHistory;

public record GetStudentAcademicHistoryQuery
(
    [Required] Guid StudentId
) : IRequest<Result<List<StudentSemesterDataResponse>>>;
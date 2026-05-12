using Universe.Application.UserServices.UserDtos;

namespace Universe.Application.UserServices.Querys.GetStudentAcademicHistory;

public record GetStudentAcademicHistoryCommand
(
    [Required] Guid StudentId
) : IRequest<Result<List<StudentSemesterDataResponse>>>;
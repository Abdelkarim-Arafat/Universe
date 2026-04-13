using Universe.Application.UserServices.UserDtos;

namespace Universe.Application.UserServices.Querys.GetStudentAcademicHistory;

public record GetStudentAcademicHistoryCommand
(
) : IRequest<Result<List<TranscriptSemesterResponse>>>;
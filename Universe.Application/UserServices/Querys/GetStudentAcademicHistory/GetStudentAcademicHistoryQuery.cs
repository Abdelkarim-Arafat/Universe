using Universe.Core.Contracts.User;

namespace Universe.Application.UserServices.Querys.GetStudentAcademicHistory;

public record GetStudentAcademicHistoryQuery
(
) : IRequest<Result<List<TranscriptSemesterResponse>>>;
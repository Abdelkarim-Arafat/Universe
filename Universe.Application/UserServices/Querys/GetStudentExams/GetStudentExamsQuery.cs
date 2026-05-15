using Universe.Core.Contracts.Student;

namespace Universe.Application.UserServices.Querys.GetStudentExams;

public record GetStudentExamsQuery
([Required] Guid StudentId) : IRequest<Result<StudentExamsResponse>>;


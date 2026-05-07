using Universe.Core.Contracts.Student;

namespace Universe.Application.UserServices.Querys.GetStudentExams;

public record GetStudentExamsQuery
() : IRequest<Result<StudentExamsResponse>>;


using Universe.Core.Dtos.Student;

namespace Universe.Application.UserServices.Querys.GetStudentExams;

public record GetStudentExamsCommand
() : IRequest<Result<StudentExamsResponse>>;


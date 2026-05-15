using Universe.Core.Contracts.Enrollments;

namespace Universe.Application.UserServices.Querys.GetStudentSchedule;

public record GetStudentScheduleQuery
([Required] Guid StudentId) : IRequest<Result<List<StudentExistingEnrollment>>>;

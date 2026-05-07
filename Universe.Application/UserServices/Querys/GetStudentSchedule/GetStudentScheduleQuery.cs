using Universe.Core.Contracts.Enrollments;

namespace Universe.Application.UserServices.Querys.GetStudentSchedule;

public record GetStudentScheduleQuery
() : IRequest<Result<List<StudentExistingEnrollment>>>;

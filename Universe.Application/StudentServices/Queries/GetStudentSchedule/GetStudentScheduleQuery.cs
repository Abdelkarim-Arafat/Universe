namespace Universe.Application.StudentServices.Queries.GetStudentSchedule;

public record GetStudentScheduleQuery(
    [Required] Guid StudentId
) : IRequest<Result<List<StudentExistingEnrollment>>>;

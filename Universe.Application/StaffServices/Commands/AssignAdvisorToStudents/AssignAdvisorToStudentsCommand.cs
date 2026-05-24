namespace Universe.Application.StaffServices.Commands.AssignAdvisorToStudents;

public record AssignAdvisorToStudentsCommand(
    [Required] Guid AdvisorId,
    [Required] List<Guid> StudentIds
) : IRequest<Result>;
namespace Universe.Application.StudentServices.Commands.UnAssignAdvisorFromStudents;

public record UnAssignAdvisorFromStudentsCommand(
    List<Guid> StudentIds
) : IRequest<Result>;
namespace Universe.Application.StudentServices.Commands.RemoveStudent;

public record RemoveStudentCommand(
    [Required] Guid ProgramId,
    [Required] Guid Id
) : IRequest<Result>;

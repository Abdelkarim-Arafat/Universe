namespace Universe.Application.StudentServices.Commands.ChangeStudentProgram;

public record ChangeStudentProgramCommand(
    [Required] Guid NewProgramId,
    [Required] Guid StudentId
) : IRequest<Result>;
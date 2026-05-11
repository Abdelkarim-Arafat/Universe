namespace Universe.Application.UserServices.Commands.ChangeStudentProgram;

public record ChangeStudentProgramCommand (
    [Required] Guid ProgramId,
    [Required] Guid StudentId
) : IRequest<Result>;
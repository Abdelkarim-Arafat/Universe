namespace Universe.Application.UserServices.Commands.ChangeStudentProgram;

public record ChangeStudentProgramCommand (
    [Required] Guid NewProgramId,
    [Required] Guid StudentId
) : IRequest<Result>;
namespace Universe.Application.LevelServices.Commands.Remove;

public record RemoveLevelCommand (
   [Required] Guid ProgramId,
   [Required] Guid Id
) : IRequest<Result>;
namespace Universe.Application.LevelServices.Commands.RemoveLevel;

public record RemoveLevelCommand (
   [Required] Guid ProgramId,
   [Required] Guid Id
) : IRequest<Result>;
namespace Universe.Application.LevelServices.Commands.Remove;

public record RemoveLevelCommand
(
   [Required] Guid Id
) : IRequest<Result>;
namespace Universe.Application.LevelServices.Commands.RemoveLevel;

public record RemoveLevelCommand
(
    Guid Id
) : IRequest<Result>;
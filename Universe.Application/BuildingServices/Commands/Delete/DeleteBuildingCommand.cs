namespace Universe.Application.BuildingServices.Commands.Delete;

public record DeleteBuildingCommand
(
    [Required]Guid Id
) : IRequest<Result>;

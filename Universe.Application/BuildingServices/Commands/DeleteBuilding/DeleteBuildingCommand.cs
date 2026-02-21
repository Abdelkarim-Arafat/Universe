namespace Universe.Application.BuildingServices.Commands.DeleteBuilding;

public record DeleteBuildingCommand
(
    [Required]Guid Id
) : IRequest<Result>;

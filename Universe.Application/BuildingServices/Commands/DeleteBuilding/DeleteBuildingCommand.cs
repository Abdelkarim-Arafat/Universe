namespace Universe.Application.BuildingServices.Commands.DeleteBuilding;

public record DeleteBuildingCommand
(
    Guid Id
) : IRequest<Result>;

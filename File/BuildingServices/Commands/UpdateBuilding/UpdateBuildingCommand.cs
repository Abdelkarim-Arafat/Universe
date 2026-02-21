namespace Universe.Application.BuildingServices.Commands.UpdateBuilding;

public record UpdateBuildingCommand
(
    Guid Id,
    string Name,
    string Code
) : IRequest<Result<BuildingResponse>>;

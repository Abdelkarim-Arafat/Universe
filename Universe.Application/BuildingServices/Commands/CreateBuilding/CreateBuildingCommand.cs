using Universe.Application.BuildingServices.Dtos;

namespace Universe.Application.BuildingServices.Commands.CreateBuilding;

public record CreateBuildingCommand
(
    string Name,
    string Code
) : IRequest<Result<BuildingResponse>>;

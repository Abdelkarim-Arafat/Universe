using Universe.Application.BuildingServices.Dtos;

namespace Universe.Application.BuildingServices.Commands.UpdateBuilding;

public record UpdateBuildingCommand
(
    [Required]Guid Id,
    string Name,
    string Code
) : IRequest<Result<BuildingResponse>>;

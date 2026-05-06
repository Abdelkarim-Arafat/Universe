using Universe.Core.Constants.Buildings;

namespace Universe.Application.BuildingServices.Commands.Update;

public record UpdateBuildingCommand
(
    Guid Id,
    string Name,
    string Code
) : IRequest<Result<BuildingResponse>>;

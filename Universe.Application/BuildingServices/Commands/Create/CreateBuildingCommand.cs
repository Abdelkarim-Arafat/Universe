
using Universe.Core.Constants.Buildings;

namespace Universe.Application.BuildingServices.Commands.Create;

public record CreateBuildingCommand
(
    string Name,
    string Code
) : IRequest<Result<BuildingResponse>>;

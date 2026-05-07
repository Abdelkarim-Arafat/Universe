
using Universe.Core.Constants.Buildings;

namespace Universe.Application.BuildingServices.Queries.Get;

public record GetBuildingQuery
(
   [Required]Guid Id
): IRequest<Result<BuildingResponse>>;

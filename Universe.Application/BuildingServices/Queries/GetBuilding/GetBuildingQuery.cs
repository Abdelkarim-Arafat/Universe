using Universe.Application.BuildingServices.Dtos;

namespace Universe.Application.BuildingServices.Queries.GetBuilding;

public record GetBuildingQuery
(
   Guid Id
): IRequest<Result<BuildingResponse>>;

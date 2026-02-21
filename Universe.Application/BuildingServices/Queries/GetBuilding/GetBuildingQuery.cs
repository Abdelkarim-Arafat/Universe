using Universe.Application.BuildingServices.Dtos;

namespace Universe.Application.BuildingServices.Queries.GetBuilding;

public record GetBuildingQuery
(
   [Required]Guid Id
): IRequest<Result<BuildingResponse>>;

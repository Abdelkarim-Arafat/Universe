using Universe.Application.BuildingServices.Dtos;

namespace Universe.Application.BuildingServices.Queries.Get;

public record GetBuildingQuery
(
   [Required]Guid Id
): IRequest<Result<BuildingResponse>>;

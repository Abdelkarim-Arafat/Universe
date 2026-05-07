using Universe.Core.Constants.Buildings;

namespace Universe.Application.BuildingServices.Queries.GetBuildings;

public record GetBuildingsQuery
(
    FilterRequest Filter
) : IRequest<Result<PaginationList<BuildingResponse>>>;

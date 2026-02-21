namespace Universe.Application.BuildingServices.Queries.GetAll;

public record GetAllQuery
(
    FilterRequest Filter
) : IRequest<Result<PaginationList<BuildingResponse>>>;

using Universe.Application.RoomTypeServices.Dtos;

namespace Universe.Application.RoomTypeServices.Queries.GetAllTypes;

public record GetAllTypesQuery
(FilterRequest Filter) : IRequest<Result<PaginationList<RoomTypeResponse>>>;

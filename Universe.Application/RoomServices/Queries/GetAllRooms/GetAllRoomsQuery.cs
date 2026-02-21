using Universe.Application.RoomServices.Dtos;

namespace Universe.Application.RoomServices.Queries.GetAllRooms;

public record GetAllRoomsQuery
(FilterRequest filter) : IRequest<Result<PaginationList<RoomResponse>>>;

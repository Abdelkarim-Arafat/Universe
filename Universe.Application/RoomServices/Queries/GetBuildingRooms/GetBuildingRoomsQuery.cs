using Universe.Application.RoomServices.Dtos;

namespace Universe.Application.RoomServices.Queries.GetBuildingRooms;

public record GetBuildingRoomsQuery
    (Guid BuildingId, FilterRequest filter) : IRequest<Result<PaginationList<RoomResponse>>>;


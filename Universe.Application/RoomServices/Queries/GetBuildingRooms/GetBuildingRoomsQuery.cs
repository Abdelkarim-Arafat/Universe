 
using Universe.Core.Contracts.Rooms;

namespace Universe.Application.RoomServices.Queries.GetBuildingRooms;

public record GetBuildingRoomsQuery
    (Guid BuildingId, FilterRequest filter) : IRequest<Result<PaginationList<RoomResponse>>>;


using Universe.Application.RoomServices.Dtos;

namespace Universe.Application.RoomServices.Queries.GetAllRooms;

public class GetAllRoomsQuery
() : IRequest<Result<List<RoomResponse>>>;

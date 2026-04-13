
using Universe.Application.RoomServices.Dtos;


namespace Universe.Application.RoomServices.Queries.GetRoom;

public record GetRoomQuery
(
 [Required]Guid Id
) : IRequest<Result<RoomResponse>>;

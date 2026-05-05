using Universe.Application.RoomServices.Dtos;

namespace Universe.Application.RoomServices.Queries.Get;

public record GetRoomQuery
(
 [Required]Guid Id
) : IRequest<Result<RoomResponse>>;

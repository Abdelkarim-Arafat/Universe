using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.RoomServices.Dtos;
using Universe.Application.RoomTypeServices.Dtos;

namespace Universe.Application.RoomServices.Queries.GetRoom;

public record GetRoomQuery
(
 [Required]Guid Id
) : IRequest<Result<RoomResponse>>;

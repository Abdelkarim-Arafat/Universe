using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.RoomTypeServices.Dtos;

namespace Universe.Application.RoomTypeServices.Queries.GetRoomType;

public record GetRoomTypeQuery
(
 Guid Id
) : IRequest<Result<RoomTypeResponse>>;

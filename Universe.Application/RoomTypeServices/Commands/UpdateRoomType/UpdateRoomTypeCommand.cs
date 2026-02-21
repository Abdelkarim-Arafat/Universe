using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.RoomTypeServices.Dtos;

namespace Universe.Application.RoomTypeServices.Commands.UpdateRoomType;

public record UpdateRoomTypeCommand(
    Guid Id,
    string Name
) : IRequest<Result<RoomTypeResponse>>;

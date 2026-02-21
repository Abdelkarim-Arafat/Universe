using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.RoomTypeServices.Dtos;

namespace Universe.Application.RoomTypeServices.Commands.CreateRoomType;

public record CreateRoomTypeCommand
(
    string Name
) : IRequest<Result<RoomTypeResponse>>;

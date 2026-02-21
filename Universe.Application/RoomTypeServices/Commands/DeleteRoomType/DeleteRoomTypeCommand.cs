using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.RoomTypeServices.Commands.DeleteRoomType;

public record DeleteRoomTypeCommand
(
    Guid Id
) : IRequest<Result>;

using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.RoomServices.Commands.DeleteRoom;

public record DeleteRoomCommand
([Required]Guid Id) : IRequest<Result>;

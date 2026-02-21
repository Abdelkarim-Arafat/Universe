using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Abstractions;

namespace Universe.Core.Errors;

public class RoomErrors
{
    public static readonly Error RoomNotFound =
        new("Room.NotFound", "No Room with specific id", StatusCodes.Status404NotFound);
    public static readonly Error RoomTypeNotFound =
        new("Room.NotFound", "No RoomType with specific id", StatusCodes.Status404NotFound);
    public static readonly Error UnvalidRoomNumber =
       new("Room.UnvalidRoomNumber", "There is another room with the same number in this building", StatusCodes.Status409Conflict);
}

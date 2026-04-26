using Microsoft.AspNetCore.Http;
using Universe.Core.Abstractions;

namespace Universe.Core.Errors;

public class RoomErrors
{
    public static readonly Error RoomNotFound =
        new("Room.NotFound", "No Room with specific id", StatusCodes.Status404NotFound);
    public static readonly Error UnvalidRoomNumber =
       new("Room.UnvalidRoomNumber", "There is another room with the same number in this building", StatusCodes.Status409Conflict);

    public static readonly Error UnValidCapacity =
        new("Room.UnValidCapacity", "This Capacity is larger than Room Capacity", StatusCodes.Status409Conflict);
}

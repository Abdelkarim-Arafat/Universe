
namespace Universe.Core.Contracts.Rooms;

public record RoomResponse
(
 Guid Id,
 string Name,
 int RoomNumber,
 int Capacity,
 string Type
);

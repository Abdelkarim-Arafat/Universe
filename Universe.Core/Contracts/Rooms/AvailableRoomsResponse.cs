namespace Universe.Core.Contracts.Rooms;

public record AvailableRoomsResponse
(
    Guid Id,
    int RoomNumber,
    int Capacity
);

namespace Universe.Application.RoomServices.Dtos;

public record AvailableRoomsResponse
(
    Guid Id,
    int RoomNumber,
    int Capacity
);

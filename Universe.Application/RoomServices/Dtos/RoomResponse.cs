
namespace Universe.Application.RoomServices.Dtos;

public record RoomResponse
(
 Guid Id,
 string Name,
 int RoomNumber,
 int Capacity,
 string Type
);

using Universe.Application.RoomServices.Dtos;

namespace Universe.Application.RoomServices.Queries.GetAvailableRoomsForExam;

public record GetAvailableRoomsForExamQuery
(
    [Required] Guid BuildingId,
    [Required] Guid CourseOfferingExamId,
    FilterRequest Filter
) : IRequest<Result<PaginationList<AvailableRoomsResponse>>>;

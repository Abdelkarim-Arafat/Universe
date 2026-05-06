
using Universe.Core.Contracts.Rooms;

namespace Universe.Application.RoomServices.Queries.GetAvailableRoomsForCommittees;

public record GetAvailableRoomsForCommitteesQuery
(
    [Required] Guid BuildingId,
    [Required] Guid examTermId,
    FilterRequest Filter
) : IRequest<Result<PaginationList<AvailableRoomsResponse>>>;

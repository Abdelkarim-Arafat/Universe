

using Universe.Core.Contracts.Rooms;

namespace Universe.Application.RoomServices.Queries.GetBuildingRooms;

public class GetBuildingRoomsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetBuildingRoomsQuery, Result<PaginationList<RoomResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<PaginationList<RoomResponse>>> Handle(GetBuildingRoomsQuery request, CancellationToken cancellationToken)
    {
        var query = _unitOfWork.Repository<Room>().GetQueryable()
            .Where(room => room.BuildingId == request.BuildingId && !room.IsDeleted);

        var filter = request.filter;

        if (!string.IsNullOrEmpty(filter.SearchValue))
            query = query.ApplySearch(filter.SearchValue, room => room.Name, room => room.RoomNumber.ToString());

        if (!string.IsNullOrEmpty(filter.SortColumn))
            query = query.OrderBy($"{filter.SortColumn} {filter.SortDirection}");
        
        var source = query.Select(room => new RoomResponse(
            room.Id,
            room.Name,
            room.RoomNumber,
            room.Capacity,
            room.RoomType.ToString()
        ));

        var response = await PaginationList<RoomResponse>
            .CreateAsync(source, filter.PageNumber, filter.PageSize, cancellationToken);

        return Result.Success(response);
    }
}

using Universe.Application.RoomServices.Dtos;

namespace Universe.Application.RoomServices.Queries.GetBuildingRooms;

public class GetBuildingRoomsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetBuildingRoomsQuery, Result<PaginationList<RoomResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<PaginationList<RoomResponse>>> Handle(GetBuildingRoomsQuery request, CancellationToken cancellationToken)
    {
        var query = _unitOfWork.Repository<Room>().GetQueryable()
            .Where(x => x.BuildingId == request.BuildingId);

        var filter = request.filter;

        if (!string.IsNullOrEmpty(filter.SearchValue))
        {
            query = query.Where(x => x.Name.Contains(filter.SearchValue));
        }

        if (!string.IsNullOrEmpty(filter.SortColumn))
        {
            query = query.OrderBy($"{filter.SortColumn} {filter.SortDirection}");
        }
        
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

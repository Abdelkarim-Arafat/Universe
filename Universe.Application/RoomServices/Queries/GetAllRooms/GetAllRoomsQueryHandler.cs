using Universe.Application.RoomServices.Dtos;

namespace Universe.Application.RoomServices.Queries.GetAllRooms;

public class GetAllRoomsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllRoomsQuery, Result<PaginationList<RoomResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<PaginationList<RoomResponse>>> Handle(GetAllRoomsQuery request, CancellationToken cancellationToken)
    {
        var query = _unitOfWork.Repository<Room>().GetQueryable();

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

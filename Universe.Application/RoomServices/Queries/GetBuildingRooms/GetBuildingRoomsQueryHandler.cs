using Universe.Core.Contracts.Rooms;
namespace Universe.Application.RoomServices.Queries.GetBuildingRooms;

public class GetBuildingRoomsQueryHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService)
    : IRequestHandler<GetBuildingRoomsQuery, Result<PaginationList<RoomResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result<PaginationList<RoomResponse>>> Handle(GetBuildingRoomsQuery request, CancellationToken cancellationToken)
    {
        if (!await _unitOfWork.BuildingRepository.IsExistAsync(request.BuildingId, cancellationToken))
            return Result.Failure<PaginationList<RoomResponse>>(BuildingErrors.NotFound);

        var filter = request.filter;
        var cacheKey = RoomCacheKeys.List(request.BuildingId, filter.SearchValue, filter.SortColumn, filter.SortDirection, filter.PageNumber, filter.PageSize);
        var tags = RoomCacheKeys.Tags(request.BuildingId);

        var response = await _cacheService.GetOrCreateAsync(
            key: cacheKey,
            factory: async () =>
            {
                var query = _unitOfWork.Repository<Room>().GetQueryable()
                    .AsNoTracking()
                    .Where(room => room.BuildingId == request.BuildingId && !room.IsDeleted);

                query = query.ApplySearch(filter.SearchValue, x => x.Name, x => x.RoomNumber.ToString());

                if (!string.IsNullOrEmpty(filter.SortColumn))
                    query = query.OrderBy($"{filter.SortColumn} {filter.SortDirection}");

                var projection = query.Select(room => new RoomResponse(
                    room.Id,
                    room.Name,
                    room.RoomNumber,
                    room.Capacity,
                    room.RoomType.ToString()
                ));

                return await PaginationList<RoomResponse>.CreateAsync(projection, filter.PageNumber, filter.PageSize, cancellationToken);
            },
            cancellationToken: cancellationToken,
            tags: tags
        );

        return Result.Success(response);
    }
}

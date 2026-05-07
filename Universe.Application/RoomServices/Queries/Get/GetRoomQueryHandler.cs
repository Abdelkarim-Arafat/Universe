using Universe.Core.Contracts.Rooms;
namespace Universe.Application.RoomServices.Queries.Get;

public class GetRoomQueryHandler(IUnitOfWork unitOfWork, ICacheService cacheService)
    : IRequestHandler<GetRoomQuery, Result<RoomResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result<RoomResponse>> Handle(GetRoomQuery query, CancellationToken cancellationToken)
    {
        var cacheKey = RoomCacheKeys.ById(query.Id);

        var response = await _cacheService.GetOrCreateAsync(
            key: cacheKey,
            factory: async () =>
            {
                var room = await _unitOfWork.RoomRepository.GetByIdAsync(query.Id, cancellationToken);

                if (room is null) return null;

                return new RoomResponse
                (
                    room.Id,
                    room.Name,
                    room.RoomNumber,
                    room.Capacity,
                    room.RoomType.ToString()
                );
            },
            cancellationToken: cancellationToken
        );

        return response is not null
            ? Result.Success(response)
            : Result.Failure<RoomResponse>(RoomErrors.NotFound);
    }
}

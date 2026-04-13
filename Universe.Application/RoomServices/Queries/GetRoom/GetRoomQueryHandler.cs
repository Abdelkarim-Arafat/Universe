using Universe.Application.RoomServices.Dtos;
 

namespace Universe.Application.RoomServices.Queries.GetRoom;

public class GetRoomQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetRoomQuery, Result<RoomResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<RoomResponse>> Handle(GetRoomQuery query, CancellationToken cancellationToken)
    {
        var room = await _unitOfWork.RoomRepository.GetByIdAsync(query.Id, cancellationToken);
        if (room is null)
            return Result.Failure<RoomResponse>(RoomErrors.RoomNotFound);

        var response = new RoomResponse
        (
            room.Id,
            room.Name,
            room.RoomNumber,
            room.Capacity,
            room.RoomType.ToString()
        );
        return Result.Success(response);
    }
}

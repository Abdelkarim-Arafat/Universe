using Universe.Application.RoomServices.Dtos;
 

namespace Universe.Application.RoomServices.Queries.GetRoom;

public class GetRoomQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetRoomQuery, Result<RoomResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<RoomResponse>> Handle(GetRoomQuery query, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.RoomRepository.GetRoomByIdIncludingRoomTypeAsync(query.Id, cancellationToken);
        if (result.IsFailure)
            return Result.Failure<RoomResponse>(result.Error);
        var room = result.Value;

        var response = new RoomResponse
        (
            room.Id,
            room.Name,
            room.RoomNumber,
            room.Capacity,
            room.RoomType.Name
        );
        return Result.Success(response);
    }
}

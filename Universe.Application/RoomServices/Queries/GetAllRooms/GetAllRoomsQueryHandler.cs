using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.RoomServices.Dtos;

namespace Universe.Application.RoomServices.Queries.GetAllRooms;

public class GetAllRoomsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllRoomsQuery, Result<List<RoomResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<List<RoomResponse>>> Handle(GetAllRoomsQuery query, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.RoomRepository.GetAllRoomsIncludingRoomTypeAsync(cancellationToken);

        var rooms = result.Value;

        var response = rooms.Select(room => new RoomResponse(
            room.Id,
            room.Name,
            room.RoomNumber,
            room.Capacity,
            room.RoomType.Name
        )).ToList();


        return Result.Success(response);
    }
}

using Universe.Core.Contracts.Rooms;

namespace Universe.Application.RoomServices.Commands.Update;

public class UpdateRoomCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateRoomCommand, Result<RoomResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<RoomResponse>> Handle(UpdateRoomCommand command, CancellationToken cancellationToken)
    {
        var room = await _unitOfWork.RoomRepository.GetByIdAsync(command.Id, cancellationToken);

        if (room is null)
            return Result.Failure<RoomResponse>(RoomErrors.RoomNotFound);

        var isSameRoomNumberExist = await _unitOfWork.RoomRepository
            .CheckValidRoomNumberAsync(room.Id, room.BuildingId, command.RoomNumber, cancellationToken);

        if (isSameRoomNumberExist)
            return Result.Failure<RoomResponse>(RoomErrors.UnvalidRoomNumber);

        command.Adapt(room);

        _unitOfWork.Repository<Room>().Update(room);

        await _unitOfWork.CompleteAsync(cancellationToken);

        var response = new RoomResponse
            (room.Id,
            room.Name,
            room.RoomNumber,
            room.Capacity,
            room.RoomType.ToString());

        return Result.Success(response);
    }
}
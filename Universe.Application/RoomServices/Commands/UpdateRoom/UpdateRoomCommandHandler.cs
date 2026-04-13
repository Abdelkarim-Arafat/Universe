using Universe.Application.RoomServices.Dtos;

namespace Universe.Application.RoomServices.Commands.UpdateRoom;

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

        room.Name = command.Name;
        room.Capacity = command.Capacity;
        room.RoomNumber = command.RoomNumber;
        room.RoomType = command.RoomType;

        _unitOfWork.Repository<Room>().Update(room);

        try
        {
            await _unitOfWork.CompleteAsync(cancellationToken);
        }
        catch (DbUpdateException)
        {

            return Result.Failure<RoomResponse>(
                new Error("DatabaseError", "failed to update room", StatusCodes.Status409Conflict));
        }


        var response = new RoomResponse
            (room.Id,
            room.Name,
            room.RoomNumber,
            room.Capacity,
            room.RoomType.ToString());

        return Result.Success(response);
    }
}
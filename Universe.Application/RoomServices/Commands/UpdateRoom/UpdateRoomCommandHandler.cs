using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.RoomServices.Commands.CreateRoom;
using Universe.Application.RoomServices.Dtos;

namespace Universe.Application.RoomServices.Commands.UpdateRoom;

public class UpdateRoomCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateRoomCommand, Result<RoomResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<RoomResponse>> Handle(UpdateRoomCommand command, CancellationToken cancellationToken)
    {
        var checkRoomExist = await _unitOfWork.RoomRepository.GetByIdAsync(command.Id, cancellationToken);

        if (checkRoomExist.IsFailure)
            return Result.Failure<RoomResponse>(RoomErrors.RoomNotFound);

        var room = checkRoomExist.Value;

        var isRoomTypeExist = await _unitOfWork.RoomTypeRepository.CheckIfRoomTypeExist(command.RoomTypeId, cancellationToken);

        if (isRoomTypeExist.IsFailure)
            return Result.Failure<RoomResponse>(RoomErrors.RoomTypeNotFound);

        var isValidRoomNumber = await _unitOfWork.RoomRepository
            .CheckValidRoomNumberAsync(room.Id, room.BuildingId, command.RoomNumber, cancellationToken);

        if (isValidRoomNumber.IsFailure)
            return Result.Failure<RoomResponse>(RoomErrors.UnvalidRoomNumber);

        room.Name = command.Name;
        room.Capacity = command.Capacity;
        room.RoomNumber = command.RoomNumber;
        room.RoomTypeId = command.RoomTypeId;

        _unitOfWork.Repository<Room>().Update(room);

        try
        {
            await _unitOfWork.CompleteAsync(cancellationToken);
        }
        catch (DbUpdateException ex)
        {

            return Result.Failure<RoomResponse>(
                new Error("DatabaseError", ex.Message, StatusCodes.Status409Conflict));
        }

        var result = await _unitOfWork.RoomRepository.GetRoomByIdIncludingRoomTypeAsync(room.Id, cancellationToken);
        room = result.Value;

        var response = new RoomResponse
            (room.Id,
            room.Name,
            room.RoomNumber,
            room.Capacity,
            room.RoomType.Name);

        return Result.Success(response);
    }
}
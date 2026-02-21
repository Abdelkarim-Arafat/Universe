using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.RoomServices.Dtos;
using Universe.Application.RoomTypeServices.Commands.CreateRoomType;
using Universe.Application.RoomTypeServices.Dtos;

namespace Universe.Application.RoomServices.Commands.CreateRoom;

public class CreateRoomCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateRoomCommand, Result<RoomResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<RoomResponse>> Handle(CreateRoomCommand command, CancellationToken cancellationToken)
    {
        var isRoomTypeExist = await _unitOfWork.RoomTypeRepository.CheckIfRoomTypeExist(command.RoomTypeId, cancellationToken);

        if (isRoomTypeExist.IsFailure)
            return Result.Failure<RoomResponse>(RoomErrors.RoomTypeNotFound);

        var isBuildingExist = await _unitOfWork.BuildingRepository.CheckIfBuildingExistAsync(command.BuildingId, cancellationToken);

        if (isBuildingExist.IsFailure)
            return Result.Failure<RoomResponse>(BuildingErrors.NotFound);

        var isValidRoomNumber = await _unitOfWork.RoomRepository.CheckValidRoomNumberAsync(command.BuildingId, command.RoomNumber, cancellationToken);

        if (isValidRoomNumber.IsFailure)
            return Result.Failure<RoomResponse>(RoomErrors.UnvalidRoomNumber);

        var room = command.Adapt<Room>();
        _unitOfWork.Repository<Room>().Add(room, cancellationToken);

        try
        {
            await _unitOfWork.CompleteAsync(cancellationToken);
        }
        catch (DbUpdateException ex)
        {

            return Result.Failure<RoomResponse>(
                new Error("DatabaseError", ex.Message, StatusCodes.Status409Conflict));
        }
        var result  = await _unitOfWork.RoomRepository.GetRoomByIdIncludingRoomTypeAsync(room.Id, cancellationToken);
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

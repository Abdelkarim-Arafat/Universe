using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.RoomTypeServices.Commands.CreateRoomType;
using Universe.Application.RoomTypeServices.Dtos;

namespace Universe.Application.RoomTypeServices.Commands.UpdateRoomType;

public class UpdateRoomTypeCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateRoomTypeCommand, Result<RoomTypeResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<RoomTypeResponse>> Handle(UpdateRoomTypeCommand command, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.RoomTypeRepository.GetByIdAsync(command.Id, cancellationToken);
        if (result.IsFailure)
            return Result.Failure<RoomTypeResponse>(result.Error);

        var type = result.Value;
        type.Name = command.Name;
        _unitOfWork.Repository<RoomType>().Update(type);

        try
        {
            await _unitOfWork.CompleteAsync(cancellationToken);
        }
        catch (DbUpdateException ex)
        {

            return Result.Failure<RoomTypeResponse>(
                new Error("DatabaseError", ex.Message, StatusCodes.Status409Conflict));
        }
        return Result.Success(type.Adapt<RoomTypeResponse>());
    }
}

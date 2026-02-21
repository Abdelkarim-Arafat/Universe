using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.RoomTypeServices.Commands.CreateRoomType;
using Universe.Application.RoomTypeServices.Dtos;

namespace Universe.Application.RoomTypeServices.Commands.DeleteRoomType;

public class DeleteRoomTypeCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteRoomTypeCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(DeleteRoomTypeCommand command, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.RoomTypeRepository.GetByIdAsync(command.Id, cancellationToken);
        if (result.IsFailure)
            return Result.Failure(result.Error);

        var type = result.Value;

        _unitOfWork.Repository<RoomType>().SoftDelete(type);
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
        return Result.Success();
    }
}

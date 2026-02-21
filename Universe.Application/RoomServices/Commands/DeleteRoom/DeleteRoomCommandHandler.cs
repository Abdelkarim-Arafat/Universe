using Universe.Application.RoomServices.Dtos;

namespace Universe.Application.RoomServices.Commands.DeleteRoom;

public class DeleteRoomCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteRoomCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(DeleteRoomCommand command, CancellationToken cancellationToken)
    {
        var room = await _unitOfWork.RoomRepository.GetByIdAsync(command.Id, cancellationToken);
        if (room is null)
            return Result.Failure(RoomErrors.RoomNotFound);
         
        _unitOfWork.Repository<Room>().SoftDelete(room);
        _unitOfWork.Repository<Room>().Update(room);

        try
        {
            await _unitOfWork.CompleteAsync(cancellationToken);
        }
        catch (DbUpdateException)
        {

            return Result.Failure<RoomResponse>(
                new Error("DatabaseError", "Failed to delete room", StatusCodes.Status409Conflict));
        }

        return Result.Success();
    }
}

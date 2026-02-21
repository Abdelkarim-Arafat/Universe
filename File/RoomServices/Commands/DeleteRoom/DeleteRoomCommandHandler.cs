using Universe.Application.RoomServices.Dtos;

namespace Universe.Application.RoomServices.Commands.DeleteRoom;

public class DeleteRoomCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteRoomCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(DeleteRoomCommand command, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.RoomRepository.GetByIdAsync(command.Id, cancellationToken);
        if (result.IsFailure)
            return Result.Failure(result.Error);
        var room = result.Value;

        _unitOfWork.Repository<Room>().SoftDelete(room);
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

        return Result.Success();
    }
}

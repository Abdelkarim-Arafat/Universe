namespace Universe.Application.RoomServices.Commands.Delete;

public class DeleteRoomCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteRoomCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(DeleteRoomCommand command, CancellationToken cancellationToken)
    {
        var room = await _unitOfWork.RoomRepository.GetByIdAsync(command.Id, cancellationToken);
        if (room is null)
            return Result.Failure(RoomErrors.RoomNotFound);

        _unitOfWork.Repository<Room>().DeletePermanently(room);

        await _unitOfWork.CompleteAsync(cancellationToken);

        return Result.Success();
    }
}

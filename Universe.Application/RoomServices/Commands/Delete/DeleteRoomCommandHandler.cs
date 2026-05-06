namespace Universe.Application.RoomServices.Commands.Delete;

public class DeleteRoomCommandHandler(IUnitOfWork unitOfWork, ICacheService cacheService)
    : IRequestHandler<DeleteRoomCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result> Handle(DeleteRoomCommand command, CancellationToken cancellationToken)
    {
       
        var room = await _unitOfWork.RoomRepository.GetByIdAsync(command.Id, cancellationToken);

        if (room is null)
            return Result.Failure(RoomErrors.NotFound);

         var buildingId = room.BuildingId;
        _unitOfWork.Repository<Room>().DeletePermanently(room);

        await _unitOfWork.CompleteAsync(cancellationToken);

        await _cacheService.RemoveByTagAsync(RoomCacheKeys.Tags(buildingId), cancellationToken);

        return Result.Success();
    }
}
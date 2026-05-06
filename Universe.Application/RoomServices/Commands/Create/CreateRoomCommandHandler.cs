using Universe.Application.RoomServices.Commands.Create;
using Universe.Core.Contracts.Rooms;

public class CreateRoomCommandHandler(IUnitOfWork unitOfWork, ICacheService cacheService)
    : IRequestHandler<CreateRoomCommand, Result<RoomResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result<RoomResponse>> Handle(CreateRoomCommand command, CancellationToken cancellationToken)
    {
        var isBuildingExist = await _unitOfWork.BuildingRepository
            .IsExistAsync(command.BuildingId, cancellationToken);

        if (!isBuildingExist)
            return Result.Failure<RoomResponse>(BuildingErrors.NotFound);

        var isSameRoomNumberExist = await _unitOfWork.RoomRepository
            .CheckValidRoomNumberAsync(null, command.BuildingId, command.RoomNumber, cancellationToken);

        if (isSameRoomNumberExist)
            return Result.Failure<RoomResponse>(RoomErrors.UnvalidNumber);

        var room = command.Adapt<Room>();
        await _unitOfWork.Repository<Room>().AddAsync(room, cancellationToken);

        await _unitOfWork.CompleteAsync(cancellationToken);

        await _cacheService.RemoveByTagAsync(RoomCacheKeys.Tags(command.BuildingId), cancellationToken);

        var response = new RoomResponse(
            room.Id,
            room.Name,
            room.RoomNumber,
            room.Capacity,
            room.RoomType.ToString());

        return Result.Success(response);
    }
}
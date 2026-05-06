using Org.BouncyCastle.Asn1.Ocsp;
using Universe.Core.Contracts.Rooms;

namespace Universe.Application.RoomServices.Commands.Update;

public class UpdateRoomCommandHandler(IUnitOfWork unitOfWork, ICacheService cacheService)
    : IRequestHandler<UpdateRoomCommand, Result<RoomResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result<RoomResponse>> Handle(UpdateRoomCommand command, CancellationToken cancellationToken)
    {
         
        var room = await _unitOfWork.RoomRepository.GetByIdAsync(command.Id, cancellationToken);
        if (room is null)
            return Result.Failure<RoomResponse>(RoomErrors.NotFound);

    
        var isSameRoomNumberExist = await _unitOfWork.RoomRepository
            .CheckValidRoomNumberAsync(room.Id, room.BuildingId, command.RoomNumber, cancellationToken);

        if (isSameRoomNumberExist)
            return Result.Failure<RoomResponse>(RoomErrors.UnvalidNumber);
 
        command.Adapt(room);
        _unitOfWork.Repository<Room>().Update(room);

        await _unitOfWork.CompleteAsync(cancellationToken);

        await _cacheService.RemoveByTagAsync(RoomCacheKeys.Tags(room.BuildingId), cancellationToken);

        var response = new RoomResponse(
            room.Id,
            room.Name,
            room.RoomNumber,
            room.Capacity,
            room.RoomType.ToString());

        return Result.Success(response);
    }
}
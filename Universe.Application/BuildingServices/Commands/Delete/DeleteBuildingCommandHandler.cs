namespace Universe.Application.BuildingServices.Commands.Delete;

public class DeleteBuildingCommandHandler(IUnitOfWork unitOfWork, ICacheService cacheService)
    : IRequestHandler<DeleteBuildingCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result> Handle(DeleteBuildingCommand command, CancellationToken cancellationToken)
    {
        var building = await _unitOfWork.BuildingRepository.GetByIdAsync(command.Id, cancellationToken);

        if (building is null)
            return Result.Failure(BuildingErrors.NotFound);

        var isRoomExistInBuilding = await _unitOfWork.BuildingRepository
            .CheckIfRoomExistAsync(command.Id, cancellationToken);

        if (isRoomExistInBuilding)
            return Result.Failure(BuildingErrors.RoomsFounded);
 
        _unitOfWork.Repository<Building>().DeletePermanently(building);
        await _unitOfWork.CompleteAsync(cancellationToken);

        await _cacheService.RemoveByTagAsync(BuildingCacheKeys.ListTags(), cancellationToken);

        return Result.Success();
    }
}


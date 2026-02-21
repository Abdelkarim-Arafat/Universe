namespace Universe.Application.BuildingServices.Commands.DeleteBuilding;

public class DeleteBuildingCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteBuildingCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(DeleteBuildingCommand command, CancellationToken cancellationToken)
    {
        var building = await _unitOfWork.BuildingRepository.GetByIdAsync(command.Id, cancellationToken);

        if (building is null)
            return Result.Failure(BuildingErrors.NotFound);

        var isRoomExistInBuilding = await _unitOfWork.BuildingRepository.CheckIfRoomExistAsync(command.Id, cancellationToken);

        if (isRoomExistInBuilding)
            return Result.Failure(BuildingErrors.RoomsFounded);


        _unitOfWork.Repository<Building>().SoftDelete(building);

        _unitOfWork.Repository<Building>().Update(building);

        try
        {
            await _unitOfWork.CompleteAsync(cancellationToken);
        }
        catch (DbUpdateException ex)
        {

            return Result.Failure<BuildingResponse>(
                new Error("DatabaseError", ex.Message, StatusCodes.Status409Conflict));
        }
        return Result.Success();
    }
}


namespace Universe.Application.BuildingServices.Commands.DeleteBuilding;

public class DeleteBuildingCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteBuildingCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(DeleteBuildingCommand command, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.BuildingRepository.GetByIdAsync(command.Id, cancellationToken);

        if (result.IsFailure)
            return Result.Failure(BuildingErrors.NotFound);

        var check = await _unitOfWork.BuildingRepository.CheckIfRoomExistAsync(command.Id, cancellationToken);

        if (check.IsFailure)
            return Result.Failure(check.Error);

        var building = result.Value;

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


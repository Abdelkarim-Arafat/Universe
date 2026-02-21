namespace Universe.Application.BuildingServices.Commands.UpdateBuilding;

public class UpdateBuildingCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateBuildingCommand, Result<BuildingResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<BuildingResponse>> Handle(UpdateBuildingCommand command, CancellationToken cancellationToken)
    {
        var building = await _unitOfWork.BuildingRepository.GetByIdAsync(command.Id, cancellationToken);
        if (building is null)
            return Result.Failure<BuildingResponse>(BuildingErrors.NotFound);

        building.Name = command.Name;
        building.Code = command.Code;

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
        return Result.Success(building.Adapt<BuildingResponse>());
    }
}

 

namespace Universe.Application.BuildingServices.Commands.CreateBuilding;

public class CreateBuildingCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateBuildingCommand, Result<BuildingResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<BuildingResponse>> Handle(CreateBuildingCommand command, CancellationToken cancellationToken)
    {
        var building = command.Adapt<Building>();
        _unitOfWork.Repository<Building>().Add(building);

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

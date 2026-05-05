using Universe.Core.Constants.Buildings;

namespace Universe.Application.BuildingServices.Commands.Create;

public class CreateBuildingCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateBuildingCommand, Result<BuildingResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<BuildingResponse>> Handle(CreateBuildingCommand command, CancellationToken cancellationToken)
    {
        var building = command.Adapt<Building>();

        await _unitOfWork.Repository<Building>().AddAsync(building, cancellationToken);

        await _unitOfWork.CompleteAsync(cancellationToken);

        return Result.Success(building.Adapt<BuildingResponse>());
    }
}

using Universe.Application.BuildingServices.Dtos;

namespace Universe.Application.BuildingServices.Queries.GetBuilding;

public class GetBuildingQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetBuildingQuery, Result<BuildingResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<BuildingResponse>> Handle(GetBuildingQuery request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.BuildingRepository.GetByIdAsync(request.Id, cancellationToken);
        if (result.IsFailure)
            return Result.Failure<BuildingResponse>(result.Error);

        var building = result.Value.Adapt<BuildingResponse>();
        return Result.Success(building);
    }
}

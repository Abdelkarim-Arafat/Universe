using Universe.Application.BuildingServices.Dtos;

namespace Universe.Application.BuildingServices.Queries.Get;

public class GetBuildingQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetBuildingQuery, Result<BuildingResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<BuildingResponse>> Handle(GetBuildingQuery request, CancellationToken cancellationToken)
    {
        var building = await _unitOfWork.BuildingRepository.GetByIdAsync(request.Id, cancellationToken);
        if (building is null)
            return Result.Failure<BuildingResponse>(BuildingErrors.NotFound);

        return Result.Success(building.Adapt<BuildingResponse>());
    }
}

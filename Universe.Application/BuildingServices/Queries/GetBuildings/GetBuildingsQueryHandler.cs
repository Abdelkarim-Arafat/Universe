using Universe.Core.Constants.Buildings;

namespace Universe.Application.BuildingServices.Queries.GetBuildings;

public class GetBuildingsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetBuildingsQuery, Result<PaginationList<BuildingResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<PaginationList<BuildingResponse>>> Handle(GetBuildingsQuery request, CancellationToken cancellationToken = default)
    {
        var query = _unitOfWork.Repository<Building>().GetQueryable();

        var filter = request.Filter;

        if (!string.IsNullOrEmpty(filter.SearchValue))
            query = query.ApplySearch(filter.SearchValue, x => x.Name);
        
        if (!string.IsNullOrEmpty(filter.SortColumn))
            query = query.OrderBy($"{filter.SortColumn} {filter.SortDirection}");
   
        var source = query.Select(x => new BuildingResponse(
            x.Id.ToString(),
            x.Name,
            x.Code
        ));

        var response = await PaginationList<BuildingResponse>
            .CreateAsync(source, filter.PageNumber, filter.PageSize, cancellationToken);

        return Result.Success(response);
    }
}
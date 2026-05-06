using Universe.Core.Constants.Buildings;

namespace Universe.Application.BuildingServices.Queries.GetBuildings;

public class GetBuildingsQueryHandler(IUnitOfWork unitOfWork, ICacheService cacheService)
    : IRequestHandler<GetBuildingsQuery, Result<PaginationList<BuildingResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result<PaginationList<BuildingResponse>>> Handle(GetBuildingsQuery request, CancellationToken cancellationToken = default)
    {
        var filter = request.Filter;
        var cacheKey = BuildingCacheKeys.List(filter);
        var tags = BuildingCacheKeys.ListTags(); // ده بيرجع ["buildings:all"]

        var response = await _cacheService.GetOrCreateAsync(
            key: cacheKey,
            factory: async () =>
            {
                var query = _unitOfWork.Repository<Building>()
                    .GetQueryable()
                    .AsNoTracking()
                    .Where(building => !building.IsDeleted);

                if (!string.IsNullOrEmpty(filter.SearchValue))
                    query = query.ApplySearch(filter.SearchValue, x => x.Name, x => x.Code);

                if (!string.IsNullOrEmpty(filter.SortColumn))
                    query = query.OrderBy($"{filter.SortColumn} {filter.SortDirection}");

                var projection = query.Select(x => new BuildingResponse(
                    x.Id,
                    x.Name,
                    x.Code
                ));

                return await PaginationList<BuildingResponse>
                    .CreateAsync(projection, filter.PageNumber, filter.PageSize, cancellationToken);
            },
            cancellationToken: cancellationToken,
            tags: tags
        );

        return Result.Success(response);
    }
}
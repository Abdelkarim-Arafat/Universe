namespace Universe.Application.BuildingServices.Queries.GetAll;

public class GetAllQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllQuery, Result<PaginationList<BuildingResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<PaginationList<BuildingResponse>>> Handle(GetAllQuery request, CancellationToken cancellationToken = default)
    {
        var query = _unitOfWork.Repository<Building>().GetQueryable();

        var filter = request.Filter;

        if (!string.IsNullOrEmpty(filter.SearchValue))
        {
            query = query.Where(x => x.Name.Contains(filter.SearchValue));
        }

        if (!string.IsNullOrEmpty(filter.SortColumn))
        {
            query = query.OrderBy($"{filter.SortColumn} {filter.SortDirection}");
        }

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
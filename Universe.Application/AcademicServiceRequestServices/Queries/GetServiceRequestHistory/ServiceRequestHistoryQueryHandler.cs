using Universe.Application.AcademicServiceRequestServices.Queries.GetServiceRequestHistory;
using Universe.Core.Contracts.ServiceRequest;
using Universe.Core.Enums;

public class GetServiceRequestHistoryQueryHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService
) : IRequestHandler<GetServiceRequestHistoryQuery, Result<PaginationList<ServiceRequestHistoryResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result<PaginationList<ServiceRequestHistoryResponse>>> Handle(
        GetServiceRequestHistoryQuery request,
        CancellationToken cancellationToken)
    {
        var filter = request.Filter;

        var cacheKey = ServiceRequestCacheKeys.HistoryList(
            request.CollegeId,
            filter.SortColumn,
            filter.SortDirection,
            filter.PageNumber,
            filter.PageSize);

        var tags = ServiceRequestCacheKeys.Tags(request.CollegeId);

        var response = await _cacheService.GetOrCreateAsync(
            key: cacheKey,
            factory: async () =>
            {
                var source = _unitOfWork.Repository<ServiceRequest>()
                    .GetQueryable()
                    .AsNoTracking()
                    .Where(x => x.Service.CollegeId == request.CollegeId &&
                                x.Status != RequestStatus.Pending)
                    .OrderByDescending(x => x.CreatedAt)
                    .Select(x => new ServiceRequestHistoryResponse(
                        x.Payment.Price,
                        x.Service.Name,
                        x.Student.Name,
                        x.Student.StudentCode,
                        x.CreatedAt,
                        x.UpdatedAt,
                        x.Status
                    ));

                return await PaginationList<ServiceRequestHistoryResponse>
                    .CreateAsync(source, filter.PageNumber, filter.PageSize, cancellationToken);
            },
            cancellationToken: cancellationToken,
            tags: tags
        );

        return Result.Success(response);
    }
}
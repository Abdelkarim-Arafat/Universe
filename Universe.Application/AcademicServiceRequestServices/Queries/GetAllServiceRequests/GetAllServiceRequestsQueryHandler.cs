using Universe.Application.AcademicServiceRequestServices.Queries.GetAllServiceRequests;
using Universe.Core.Contracts.ServiceRequest;
using Universe.Core.Enums;

public class GetAllServiceRequestsQueryHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService
) : IRequestHandler<GetAllServiceRequestsQuery, Result<PaginationList<ServiceRequestResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result<PaginationList<ServiceRequestResponse>>> Handle(
        GetAllServiceRequestsQuery request,
        CancellationToken cancellationToken)
    {
        var filter = request.Filter;

        var cacheKey = ServiceRequestCacheKeys.PendingList(
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
                var query = _unitOfWork.Repository<ServiceRequest>()
                    .GetQueryable()
                    .AsNoTracking()
                    .Where(x => x.Service.CollegeId == request.CollegeId &&
                                x.Status == RequestStatus.Pending)
                    .OrderBy(x => x.CreatedAt)
                    .Select(x => new ServiceRequestResponse(
                        x.Id,
                        x.Payment.Price,
                        x.Service.Name,
                        x.Student.Name,
                        x.Student.StudentCode,
                        x.CreatedAt
                    ));

                return await PaginationList<ServiceRequestResponse>
                    .CreateAsync(query, filter.PageNumber, filter.PageSize, cancellationToken);
            },
            cancellationToken: cancellationToken,
            tags: tags
        );
        return Result.Success(response);
    }
}
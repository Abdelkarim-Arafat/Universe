using System.Security.Claims;
using Universe.Application.AcademicServiceRequestServices.Queries.GetStudentServiceRequestHistory;
using Universe.Core.Contracts.ServiceRequest;

public class GetStudentServiceRequestHistoryQueryHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService,
    IHttpContextAccessor httpContext
) : IRequestHandler<GetStudentServiceRequestHistoryQuery, Result<PaginationList<ServiceRequestHistoryResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    private readonly IHttpContextAccessor _httpContext = httpContext;

    public async Task<Result<PaginationList<ServiceRequestHistoryResponse>>> Handle(
        GetStudentServiceRequestHistoryQuery request,
        CancellationToken cancellationToken)
    {
        var userId = _httpContext.HttpContext!.User
            .FindFirst(ClaimTypes.NameIdentifier)!.Value;

        var filter = request.Filter;

        var cacheKey = ServiceRequestCacheKeys.StudentHistory(
            Guid.Parse(userId),
            filter.SortColumn,
            filter.SortDirection,
            filter.PageNumber,
            filter.PageSize);

        var response = await _cacheService.GetOrCreateAsync(
            key: cacheKey,
            factory: async () =>
            {
                var query = _unitOfWork.Repository<ServiceRequest>()
                    .GetQueryable()
                    .AsNoTracking()
                    .Where(x => x.StudentId == Guid.Parse(userId))
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
                    .CreateAsync(query, filter.PageNumber, filter.PageSize, cancellationToken);
            },
            cancellationToken: cancellationToken
        );

        return Result.Success(response);
    }
}
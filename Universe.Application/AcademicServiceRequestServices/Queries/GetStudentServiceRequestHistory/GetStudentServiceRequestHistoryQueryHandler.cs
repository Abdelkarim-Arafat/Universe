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
        var userId = Guid.Parse(_httpContext.HttpContext!.User
            .FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var filter = request.Filter;
        var cacheKey = ServiceRequestCacheKeys.StudentHistory(userId, filter);

        var response = await _cacheService.GetOrCreateAsync(
            key: cacheKey,
            factory: async () =>
            {
                var query = _unitOfWork.Repository<ServiceRequest>()
                    .GetQueryable()
                    .AsNoTracking()
                    .Where(x => x.StudentId == userId);

                if (!string.IsNullOrEmpty(filter.SearchValue))
                    query = query.Where(x => x.Service.Name.Contains(filter.SearchValue));
               
                if (!string.IsNullOrEmpty(filter.SortColumn))
                    query = query.OrderBy($"{filter.SortColumn} desc");

                var sourse = query.Select(x => new ServiceRequestHistoryResponse(
                        x.Payment.Price,
                        x.Service.Name,
                        x.Student.Name,
                        x.Student.StudentCode,
                        x.CreatedAt,
                        x.UpdatedAt,
                        x.Status
                    ));

                return await PaginationList<ServiceRequestHistoryResponse>
                    .CreateAsync(sourse, filter.PageNumber, filter.PageSize, cancellationToken);
            },
            cancellationToken: cancellationToken
        );

        return Result.Success(response);
    }
}
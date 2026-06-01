using Microsoft.EntityFrameworkCore;
using Universe.Core.Contracts.Notification;

namespace Universe.Application.NotificationServices.Queries.GetNotifications;

internal class GetNotificationsQueryHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService
    ) : IRequestHandler<GetNotificationsQuery, Result<PaginationList<NotificationResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result<PaginationList<NotificationResponse>>> Handle(GetNotificationsQuery request, CancellationToken cancellationToken)
    {
        var filter = request.Filter;

        var cacheKey = NotificationCacheKeys.List(request.UserId, filter);
        var tags = NotificationCacheKeys.Tags(request.UserId);

        var response = await _cacheService.GetOrCreateAsync(
            key: cacheKey,
            factory: async () =>
            {
                var query = _unitOfWork
                    .Repository<Notification>()
                    .GetQueryable()
                    .AsNoTracking()
                    .Where(x => x.UserId == request.UserId);

                var source = query
                    .OrderByDescending(x => x.CreatedAt)
                    .Select(x => new NotificationResponse(
                        x.Id,
                        x.Type,
                        x.Title,
                        x.Body,
                        x.RelatedId,
                        x.IsSeen,
                        x.CreatedAt
                    ));

                return await PaginationList<NotificationResponse>
                        .CreateAsync(source, filter.PageNumber, filter.PageSize, cancellationToken);
            },
            cancellationToken: cancellationToken,
            tags: tags
        );

        return Result.Success(response);
    }
}
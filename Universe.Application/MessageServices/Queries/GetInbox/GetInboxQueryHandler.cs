using Microsoft.EntityFrameworkCore;
using Universe.Core.Contracts.Message;

namespace Universe.Application.MessageServices.Queries.GetInbox;

public class GetInboxQueryHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService
    ) : IRequestHandler<GetInboxQuery, Result<PaginationList<MessageResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result<PaginationList<MessageResponse>>> Handle(GetInboxQuery request, CancellationToken cancellationToken)
    {
        var filter = request.Filter;

        var cacheKey = MessageCacheKeys.Inbox(request.UserId, filter);
        var tags = MessageCacheKeys.Tags(request.UserId);

        var response = await _cacheService.GetOrCreateAsync (
            key: cacheKey,
            factory: async () =>
            {
                var query = _unitOfWork
                    .Repository<Message>()
                    .GetQueryable()
                    .AsNoTracking()
                    .Where(x => x.ReceiverId == request.UserId && !x.IsDeletedByReceiver);

                if (!string.IsNullOrWhiteSpace(filter.SearchValue))
                {
                    query = query.Where(x => x.Subject.Contains(filter.SearchValue) || x.Body.Contains(filter.SearchValue));
                }

                var source = query.OrderByDescending(x => x.CreatedAt)
                .Select(x => new MessageResponse(
                    x.Id,
                    x.Subject,
                    x.Body,
                    x.SenderId,
                    x.Sender.Name,
                    x.CreatedAt,
                    x.IsRead
                ));

                return await PaginationList<MessageResponse>.CreateAsync(source,filter.PageNumber,filter.PageSize,cancellationToken);
            },
            cancellationToken: cancellationToken,
            tags: tags
        );

        return Result.Success(response);
    }
}
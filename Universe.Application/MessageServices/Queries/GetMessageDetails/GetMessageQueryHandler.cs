using Universe.Application.MessageServices.Queries.GetMessageDetails;
using Universe.Core.Contracts.Message;

internal class GetMessageQueryHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService
    ) : IRequestHandler<GetMessageQuery, Result<MessageWithRepliesResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result<MessageWithRepliesResponse>> Handle(GetMessageQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = MessageCacheKeys.ById(request.MessageId, request.UserId);

        var tags = MessageCacheKeys.Tags(request.UserId);

        var response = await _cacheService.GetOrCreateAsync(
            key: cacheKey,
            factory: async () =>
            {
                var message = await _unitOfWork
                    .Repository<Message>()
                    .GetQueryable()
                    .AsNoTracking()
                    .Include(x => x.Sender)
                    .Include(x => x.Replies)
                        .ThenInclude(x => x.Sender)
                    .FirstOrDefaultAsync(
                        x => x.Id == request.MessageId &&
                        (
                            (x.SenderId == request.UserId && !x.IsDeletedBySender) ||
                            (x.ReceiverId == request.UserId && !x.IsDeletedByReceiver)
                        ),
                        cancellationToken
                    );

                if (message is null) return null;

                if (message.SenderId != request.UserId && message.ReceiverId != request.UserId) return null;


                return new MessageWithRepliesResponse(
                    message.Id,
                    message.Subject,
                    message.Body,
                    message.SenderId,
                    message.Sender.Name,
                    message.ReceiverId,
                    message.CreatedAt,
                    message.IsRead,
                    message.Replies
                        .Where(x =>
                            (x.SenderId == request.UserId && !x.IsDeletedBySender) ||
                            (x.ReceiverId == request.UserId && !x.IsDeletedByReceiver)
                        )
                        .OrderBy(x => x.CreatedAt)
                        .Select(x => new MessageReplyResponse(
                            x.Id,
                            x.SenderId,
                            x.ReceiverId,
                            x.ParentMessageId,
                            x.Sender.Name,
                            x.Body,
                            x.IsRead,
                            x.CreatedAt
                        ))
                        .ToList()
                );
            },
            cancellationToken: cancellationToken,
            tags: tags
        );

        if (response is null)
            return Result.Failure<MessageWithRepliesResponse>(MessageErrors.NotFound);

        return Result.Success(response);
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.MessageServices.Commands.DeleteMessage;

internal class DeleteMessageCommandHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService
    ) : IRequestHandler<DeleteMessageCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
    {
        var message = await _unitOfWork
            .Repository<Message>()
            .GetQueryable()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.MessageId,cancellationToken);

        if (message is null)
            return Result.Failure(MessageErrors.NotFound);

        if (message.SenderId != request.UserId && message.ReceiverId != request.UserId)
            return Result.Failure(MessageErrors.UnAuthorized);

        if (message.SenderId == request.UserId)
            message.IsDeletedBySender = true;

        if (message.ReceiverId == request.UserId)
            message.IsDeletedByReceiver = true;

        await _unitOfWork.CompleteAsync(cancellationToken);

        await _cacheService.RemoveByTagAsync(MessageCacheKeys.Tags(message.SenderId), cancellationToken);
        await _cacheService.RemoveByTagAsync(MessageCacheKeys.Tags(message.ReceiverId), cancellationToken);

        return Result.Success();
    }
}
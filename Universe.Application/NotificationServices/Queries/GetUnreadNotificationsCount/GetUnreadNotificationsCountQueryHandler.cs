using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.NotificationServices.Queries.GetUnreadNotificationsCount;

internal class GetUnreadNotificationsCountQueryHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetUnreadNotificationsCountQuery, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<int>> Handle(GetUnreadNotificationsCountQuery request, CancellationToken cancellationToken)
    {
        if (!await _unitOfWork.UserRepository
            .IsUserExistAsync(request.UserId, cancellationToken)
            ) return Result.Failure<int>(UserErrors.UserNotFound);


        var count = await _unitOfWork.NotificationRepository
            .CountUnSeenNotificationsAsync(request.UserId, cancellationToken);

        return Result.Success(count);
    }
}
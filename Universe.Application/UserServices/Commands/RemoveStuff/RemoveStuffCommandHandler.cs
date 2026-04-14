using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.UserServices.Commands.RemoveStuff;

namespace Universe.Application.UserServices.Commands.RemoveStuff;

public class RemoveStuffCommandHandler(
    IUnitOfWork unitOfWork,
    UserManager<ApplicationUser> userManager
    ) : IRequestHandler<RemoveStuffCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<Result> Handle(RemoveStuffCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users
            .FirstOrDefaultAsync(x => x.Id == request.Id && !x.IsDeleted , cancellationToken);

        if (user is null) return Result.Failure(AuthErrors.UserNotFound);

        user.IsDeleted = true;
        user.DeletedAt = DateTime.UtcNow;

        await _userManager.UpdateAsync(user);
        await _unitOfWork.CompleteAsync(cancellationToken);

        return Result.Success();
    }
}


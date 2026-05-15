using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.CourseOfferingServices.Commands.RemoveCourseOeffering;

namespace Universe.Application.UserServices.Commands.RemoveStudent;


public class RemoveStudentCommandHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService,
    UserManager<ApplicationUser> userManager
    ) : IRequestHandler<RemoveStudentCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<Result> Handle(RemoveStudentCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users
            .Include(x => x.Student)
            .FirstOrDefaultAsync(x => x.Id == request.Id && !x.IsDeleted);

        if (user is null) return Result.Failure(StudentErrors.NotFound);

        user.IsDeleted = true;
        user.DeletedAt = DateTime.UtcNow;

        _unitOfWork.Repository<Student>().SoftDelete(user.Student);
        await _unitOfWork.CompleteAsync(cancellationToken);

        await _cacheService.RemoveByTagAsync(StudentCacheKeys.Tags(request.ProgramId), cancellationToken);

        return Result.Success();
    }
}

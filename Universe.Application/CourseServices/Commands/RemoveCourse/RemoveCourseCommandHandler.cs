using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.CourseServices.Commands.AddCourse;

namespace Universe.Application.CourseServices.Commands.RemoveCourse;

internal class RemoveCourseCommandHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService
    ) : IRequestHandler<RemoveCourseCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result> Handle(RemoveCourseCommand request, CancellationToken cancellationToken)
    {
        if(await _unitOfWork.CourseRepository
            .GetByIdAsync(request.Id, cancellationToken) is not { } course
            ) return Result.Failure(CourseErrors.CourseNotFound);

        _unitOfWork.Repository<Course>().SoftDelete(course);

        await _unitOfWork.CompleteAsync(cancellationToken);

        await _cacheService.RemoveAsync(CourseCacheKeys.ById(request.Id), cancellationToken);
        await _cacheService.RemoveByTagAsync(CourseCacheKeys.Tags(request.CollegeId), cancellationToken);

        return Result.Success();
    }
}

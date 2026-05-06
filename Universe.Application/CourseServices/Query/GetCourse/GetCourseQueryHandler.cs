using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.AcademicProgram;
using Universe.Core.Contracts.Course;

namespace Universe.Application.CourseServices.Query.GetCourse;

public class GetCourseQueryHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService
    ) : IRequestHandler<GetCourseQuery, Result<CourseWithPreRequisiteResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<CourseWithPreRequisiteResponse>> Handle(GetCourseQuery request, CancellationToken cancellationToken)
    {
        if(await _unitOfWork.CollegeRepository
            .IsExistAsync(request.CollegeId , cancellationToken) is false
            ) return Result.Failure<CourseWithPreRequisiteResponse>(CollegeErrors.NotFound);

        var courseResponse = await _cacheService.GetOrCreateAsync(
            key: CourseCacheKeys.ById(request.Id),
            factory: async () => await _unitOfWork.CourseRepository
                .GetCourseWithPrerequisitesAsync(request.Id, cancellationToken),
            cancellationToken: cancellationToken
        );

        if (courseResponse is null)
            return Result.Failure<CourseWithPreRequisiteResponse>(CourseErrors.CourseNotFound);

        return Result.Success(courseResponse);
    }
}

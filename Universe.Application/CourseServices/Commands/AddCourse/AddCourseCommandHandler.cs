using Universe.Core.Contracts.Course;

namespace Universe.Application.CourseServices.Commands.AddCourse;

public class AddCourseCommandHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService
    ) : IRequestHandler<AddCourseCommand, Result<CourseWithPreRequisiteResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result<CourseWithPreRequisiteResponse>> Handle(AddCourseCommand request, CancellationToken cancellationToken)
    {
        var isExist = await _unitOfWork.CourseRepository
            .IsExistAsync(request.CollegeId, request.Name, request.Code, null, cancellationToken);

        if (isExist) return Result.Failure<CourseWithPreRequisiteResponse>(CourseErrors.CourseAlreadyExists);

        var existingPrerequisites = await _unitOfWork.CourseRepository
            .ExistingPreRequisitesIdsAsync(request.PreRequisiteIds, cancellationToken);

        if(existingPrerequisites.Count != request.PreRequisiteIds.Count)
            return Result.Failure<CourseWithPreRequisiteResponse>(CourseErrors.PrerequisiteNotFound);

        var course = request.Adapt<Course>();
        await _unitOfWork.Repository<Course>().AddAsync(course, cancellationToken);

        foreach (var preReqId in existingPrerequisites)
            await _unitOfWork.Repository<CoursePrerequisite>()
                 .AddAsync(new CoursePrerequisite
                {
                    CourseId = course.Id,
                    PrerequisiteCourseId = preReqId
                } , cancellationToken);

        await _unitOfWork.CompleteAsync(cancellationToken);

        await _cacheService.RemoveByTagAsync(CourseCacheKeys.Tags(course.CollegeId), cancellationToken);

        var response = await _unitOfWork.CourseRepository
                    .GetCourseWithPrerequisitesAsync(course.Id, cancellationToken);

        return Result.Success(response!);
    }
}

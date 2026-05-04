using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.Course;

namespace Universe.Application.CourseServices.Commands.UpdateCourse;

public class UpdateCourseCommandHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService
    ) : IRequestHandler<UpdateCourseCommand, Result<CourseWithPreRequisiteResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result<CourseWithPreRequisiteResponse>> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.CourseRepository
            .IsExistAsync(request.CollegeId, request.Name, request.Code, request.Id, cancellationToken))
        {
            return Result.Failure<CourseWithPreRequisiteResponse>(CourseErrors.CourseAlreadyExists);
        }

        var existingCoursesIds = await _unitOfWork.CourseRepository
            .ExistingPreRequisitesIdsAsync(request.PreRequisiteIds, cancellationToken);

        if (existingCoursesIds.Count != request.PreRequisiteIds.Count)
            return Result.Failure<CourseWithPreRequisiteResponse>(CourseErrors.PrerequisiteNotFound);


        var course = await _unitOfWork.CourseRepository
            .GetByIdAsync(request.Id, cancellationToken);

        if (course is null) return Result.Failure<CourseWithPreRequisiteResponse>(CourseErrors.CourseNotFound);

        foreach (var preReqId in existingCoursesIds)
        {
            if (preReqId == course.Id)
                return Result.Failure<CourseWithPreRequisiteResponse>(CourseErrors.SameCourse);

            if (await _unitOfWork.CourseRepository.IsExistCoursePreRequisiteAsync(preReqId, course.Id, cancellationToken))
                return Result.Failure<CourseWithPreRequisiteResponse>(CourseErrors.PrerequisiteCycleDetected);
        }
        
        var directPreReqIds = await _unitOfWork.CourseRepository
            .GetDirectPreRequisitesIdsAsync(course.Id, cancellationToken);

        var toAdd = request.PreRequisiteIds.Except(directPreReqIds).ToList();
        var toRemove = directPreReqIds.Except(request.PreRequisiteIds).ToList();

        foreach (var removeId in toRemove)
            await _unitOfWork.CourseRepository
                .RemovePrerequisiteAsync(course.Id, removeId, cancellationToken);


        foreach (var addId in toAdd)
            await _unitOfWork.Repository<CoursePrerequisite>()
                .AddAsync(new CoursePrerequisite
                {
                    CourseId = course.Id,
                    PrerequisiteCourseId = addId
                }, cancellationToken);


        request.Adapt(course);

        _unitOfWork.Repository<Course>().Update(course);
        await _unitOfWork.CompleteAsync(cancellationToken);

        var cacheKey = CourseCacheKeys.ById(course.Id);
        var tags = CourseCacheKeys.Tags(request.CollegeId);

        await _cacheService.RemoveAsync(cacheKey, cancellationToken);
        await _cacheService.RemoveByTagAsync(tags, cancellationToken);

        var response = await _cacheService.GetOrCreateAsync(
            key: cacheKey,
            factory: async () => await _unitOfWork.CourseRepository
                    .GetCourseWithPrerequisitesAsync(course.Id, cancellationToken),
            cancellationToken: cancellationToken
            );

        return Result.Success(response!);
    }
}

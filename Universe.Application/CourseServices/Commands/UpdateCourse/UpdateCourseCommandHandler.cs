using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.CourseServices.Dtos;

namespace Universe.Application.CourseServices.Commands.UpdateCourse;

public class UpdateCourseCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateCourseCommand, Result<CourseResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<CourseResponse>> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.CourseRepository
            .IsExistAsync(request.CollegeId, request.Name, request.Code, request.Id, cancellationToken))
        {
            return Result.Failure<CourseResponse>(CourseErrors.CourseAlreadyExists);
        }

        var existingCoursesIds = await _unitOfWork.CourseRepository
            .ExistingPreRequisitesIdsAsync(request.PreRequisiteIds, cancellationToken);

        if (existingCoursesIds.Count != request.PreRequisiteIds.Count)
            return Result.Failure<CourseResponse>(CourseErrors.PrerequisiteNotFound);


        var course = await _unitOfWork.CourseRepository
            .GetByIdAsync(request.Id, cancellationToken);

        if (course is null) return Result.Failure<CourseResponse>(CourseErrors.CourseNotFound);

        foreach (var preReqId in existingCoursesIds)
        {
            if (preReqId == course.Id)
                return Result.Failure<CourseResponse>(CourseErrors.SameCourse);

            if (await _unitOfWork.CourseRepository.IsExistCoursePreRequisiteAsync(preReqId, course.Id, cancellationToken))
                return Result.Failure<CourseResponse>(CourseErrors.PrerequisiteCycleDetected);
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

        var response = new CourseResponse(
            course.Id.ToString(),
            course.Name,
            course.Description,
            course.Code,
            (await _unitOfWork.CourseRepository
                .GetAllPreRequisiteAsync(course.Id, cancellationToken))
                .Adapt<List<CourseResponse>>()
        );

        return Result.Success(response);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.CourseServices.Dtos;

namespace Universe.Application.CourseServices.Commands.AddCoursePrerequisite;

public class AddCoursePreRequisiteCommandHandler(
        IUnitOfWork unitOfWork
    ) : IRequestHandler<AddCoursePreRequisiteCommand, Result<CourseResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<CourseResponse>> Handle(AddCoursePreRequisiteCommand request, CancellationToken cancellationToken)
    {
        var course = await _unitOfWork.CourseRepository.GetByIdAsync(request.CourseId , cancellationToken);

        var preRequisiteCourse = await _unitOfWork.CourseRepository.GetByIdAsync(request.PreRequisiteId , cancellationToken);

        if (course is null || preRequisiteCourse is null)
            return Result.Failure<CourseResponse>(CourseErrors.CourseNotFound);

        if (request.CourseId == request.PreRequisiteId)
            return Result.Failure<CourseResponse>(CourseErrors.SameCourse);

        if (await _unitOfWork.CourseRepository
            .IsExistCoursePreRequisiteAsync(request.CourseId , request.PreRequisiteId , cancellationToken))
            return Result.Failure<CourseResponse>(CourseErrors.PrerequisiteAlreadyExists);

        if(await _unitOfWork.CourseRepository.IsExistCoursePreRequisiteAsync(request.PreRequisiteId , request.CourseId , cancellationToken))
            return Result.Failure<CourseResponse>(CourseErrors.PrerequisiteCycleDetected);

        var coursePreRequisite = new CoursePrerequisite
        {
            CourseId = request.CourseId,
            PrerequisiteCourseId = request.PreRequisiteId
        };
        await _unitOfWork.Repository<CoursePrerequisite>().AddAsync(coursePreRequisite , cancellationToken);

        await _unitOfWork.CompleteAsync(cancellationToken);

        return Result.Success(preRequisiteCourse.Adapt<CourseResponse>());
    }
}

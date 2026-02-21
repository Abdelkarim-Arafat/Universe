using MimeKit.Cryptography;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.CourseServices.Dtos;

namespace Universe.Application.CourseServices.Commands.AddCourse;

internal class AddCourseCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddCourseCommand, Result<CourseResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<CourseResponse>> Handle(AddCourseCommand request, CancellationToken cancellationToken)
    {
        var isExist = await _unitOfWork.CourseRepository
            .IsExistAsync(request.CollegeId, request.Name, request.Code, null, cancellationToken);

        if (isExist) return Result.Failure<CourseResponse>(CourseErrors.CourseAlreadyExists);

        var existingPrerequisites = await _unitOfWork.CourseRepository
            .ExistingPreRequisitesIdsAsync(request.PreRequisiteIds, cancellationToken);

        if(existingPrerequisites.Count != request.PreRequisiteIds.Count)
            return Result.Failure<CourseResponse>(CourseErrors.PrerequisiteNotFound);

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

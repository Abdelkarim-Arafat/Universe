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

        var course = request.Adapt<Course>();

        await _unitOfWork.Repository<Course>().AddAsync(course, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);

        return Result.Success(course.Adapt<CourseResponse>());
    }
}

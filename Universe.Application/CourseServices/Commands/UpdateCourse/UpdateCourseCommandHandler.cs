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


        var Course = await _unitOfWork.CourseRepository
            .GetByIdAsync(request.Id, cancellationToken);

        if (Course is null) return Result.Failure<CourseResponse>(CourseErrors.CourseNotFound);

        request.Adapt(Course);

        _unitOfWork.Repository<Course>().Update(Course);
        await _unitOfWork.CompleteAsync(cancellationToken);

        return Result.Success(Course.Adapt<CourseResponse>());
    }
}

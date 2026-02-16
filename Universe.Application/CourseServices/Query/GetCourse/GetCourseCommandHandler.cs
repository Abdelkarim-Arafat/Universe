using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.CourseServices.Dtos;

namespace Universe.Application.CourseServices.Query.GetCourse;

public class GetCourseCommandHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetCourseCommand, Result<CourseResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<CourseResponse>> Handle(GetCourseCommand request, CancellationToken cancellationToken)
    {
        if ((await _unitOfWork.CourseRepository.GetByIdAsync(request.Id, cancellationToken)) is not { } course)
            return Result.Failure<CourseResponse>(CourseErrors.CourseNotFound);

        return Result.Success(course.Adapt<CourseResponse>());
    }
}

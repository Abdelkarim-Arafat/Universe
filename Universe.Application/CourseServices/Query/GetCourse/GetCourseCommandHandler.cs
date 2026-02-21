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

using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.CourseServices.Commands.AddCourse;

namespace Universe.Application.CourseServices.Commands.RemoveCourse;

internal class RemoveCourseCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<RemoveCourseCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(RemoveCourseCommand request, CancellationToken cancellationToken)
    {
        var Course = await _unitOfWork.CourseRepository.GetByIdAsync(request.Id, cancellationToken);
        if (Course is null) return Result.Failure(CourseErrors.CourseNotFound);

        _unitOfWork.Repository<Course>().SoftDelete(Course);

        await _unitOfWork.CompleteAsync(cancellationToken);

        return Result.Success();
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.CourseServices.Commands.RemoveCoursePrerequisite;

internal class RemoveCoursePrerequisiteCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<RemoveCoursePrerequisiteCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(RemoveCoursePrerequisiteCommand request, CancellationToken cancellationToken)
    {
        var coursePreRequisite = await _unitOfWork.CourseRepository
            .GetCoursePreRequisiteAsync(request.CourseId , request.PreRequisiteId, cancellationToken);

        if (coursePreRequisite is null)
            return Result.Failure(CourseErrors.PrerequisiteNotFound);

        _unitOfWork.Repository<CoursePrerequisite>().DeletePermanently(coursePreRequisite);

        await _unitOfWork.CompleteAsync(cancellationToken);

        return Result.Success();
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.ControlServices.Commands.ToggleCourseOfferingControl;

public class ToggleCourseOfferingControlCommandHandler(
    IUnitOfWork unitOfWork
) : IRequestHandler<ToggleCourseOfferingControlCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(ToggleCourseOfferingControlCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.CourseOfferingRepository
            .GetByIdAsync(request.CourseOfferingId, cancellationToken) is not { } course
            ) return Result.Failure(CourseOfferingErrors.NotFound);

        course.IsOpenForControl = request.IsOpenForControl;

        _unitOfWork.Repository<CourseOffering>().Update(course);
        await _unitOfWork.CompleteAsync(cancellationToken);

        return Result.Success();
    }
}

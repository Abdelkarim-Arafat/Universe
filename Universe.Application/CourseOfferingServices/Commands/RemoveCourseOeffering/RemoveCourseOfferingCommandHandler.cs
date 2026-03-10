using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.CourseOfferingServices.Commands.RemoveCourseOeffering;

public class RemoveCourseOfferingCommandHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<RemoveCourseOfferingCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(RemoveCourseOfferingCommand request, CancellationToken cancellationToken)
    {
        if ((await _unitOfWork.CourseOfferingRepository
            .GetByIdAsync(request.Id, cancellationToken)) is not { } course
            ) return Result.Failure(CourseOfferingErrors.NotFound);

        _unitOfWork.Repository<CourseOffering>().SoftDelete(course);

        return Result.Success();
    }
}

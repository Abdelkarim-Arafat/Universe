using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.CourseServices.Dtos;

namespace Universe.Application.CourseServices.Query.GetCoursePrerequisite;

internal class GetCoursePreRequisiteCommandHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetCoursePreRequisiteCommand, Result<List<CourseResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<List<CourseResponse>>> Handle(GetCoursePreRequisiteCommand request, CancellationToken cancellationToken)
    {
        var coursePreRequisites = await _unitOfWork.CourseRepository.GetAllPreRequisiteAsync(request.Id, cancellationToken);

        return Result.Success(coursePreRequisites.Adapt<List<CourseResponse>>());
    }
}

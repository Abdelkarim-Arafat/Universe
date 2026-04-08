using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.CourseOfferingServices.Dtos;

namespace Universe.Application.CourseOfferingServices.Query.GetLevelCourses;

public class GetLevelCoursesCommandHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetLevelCoursesCommand , Result<List<CourseOfferingResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<List<CourseOfferingResponse>>> Handle(GetLevelCoursesCommand request, CancellationToken cancellationToken)
    {
        var response = await _unitOfWork.Repository<CourseOffering>()
            .GetQueryable()
            .AsNoTracking()
            .Where(x =>
                x.LevelId == request.LevelId &&
                x.SemesterId == request.SemesterId &&
                !x.IsDeleted)
            .Select(x => new CourseOfferingResponse(
                x.Id.ToString(),
                x.Course.Name,
                x.Course.Code,
                x.NumberOfGroups
            ))
            .ToListAsync(cancellationToken);

        return Result.Success(response);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
 using Universe.Core.Contracts.CourseOfferings;

namespace Universe.Application.CourseOfferingServices.Queries.GetLevelCourses;

public class GetLevelCoursesCommandHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetLevelCoursesCommand , Result<List<CourseOfferingResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<List<CourseOfferingResponse>>> Handle(GetLevelCoursesCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.AcademicYearRepository
            .GetSemesterByTypeAsync(request.AcademicYearId, request.SemesterType, cancellationToken) is not { } semester
            ) return Result.Failure<List<CourseOfferingResponse>>(AcademicYearErrors.NotFound);

        var response = await _unitOfWork.Repository<CourseOffering>()
            .GetQueryable()
            .AsNoTracking()
            .Where(x =>
                x.LevelId == request.LevelId &&
                x.SemesterId == semester.Id &&
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

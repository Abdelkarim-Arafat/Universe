using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.ControlServices.Dtos;
using Universe.Core.Enums;

namespace Universe.Application.ControlServices.Queries.GetLevelsCoursesStatistics;
public class GetLevelsCoursesStatisticsQueryHandler(
    IUnitOfWork unitOfWork
) : IRequestHandler<GetLevelsCoursesStatisticsQuery, Result<List<LevelCoursesStatisticsResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<List<LevelCoursesStatisticsResponse>>> Handle(
        GetLevelsCoursesStatisticsQuery request,
        CancellationToken cancellationToken)
    {
        var data = await _unitOfWork.Repository<Level>()
            .GetQueryable()
            .AsNoTracking()
            .Where(l => l.AcademicProgramId == request.ProgramId
                && l.CourseOfferings.Any(co => co.SemesterId == request.SemesterId)
                && !l.IsDeleted)
            .Select(l => new LevelCoursesStatisticsResponse
            (
                l.Id,
                l.Name,
                l.CourseOfferings
                    .Where(co => co.SemesterId == request.SemesterId && !co.IsDeleted)
                    .Select(co => new CourseOfferingStatisticsResponse
                    (
                        co.Id,
                        co.Course.Name,
                        co.Course.Code,
                        co.Enrollments
                            .Count(e => !e.IsDeleted && e.Status == EnrollmentStatus.InProgress),

                        co.Enrollments
                            .Where(e => !e.IsDeleted)
                            .Count(e => e.Student.StudentAssessments
                                .Any(sa => sa.CourseOfferingAssessment.CourseOfferingId == co.Id
                                           && !sa.IsDeleted
                                           && !sa.degree.HasValue))
                    )).ToList()
            ))
            .ToListAsync(cancellationToken);

        return Result.Success(data);
    }
}
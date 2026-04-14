using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.ControlServices.Dtos;
using Universe.Core.Enums;

namespace Universe.Application.ControlServices.Queries.GetLevelsCoursesStatistics;

public class GetCourseOfferingsControlStatisticsQueryHandler(
    IUnitOfWork unitOfWork
) : IRequestHandler<GetCourseOfferingsControlStatisticsQuery, Result<List<GetCourseOfferingsControlStatisticsResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<List<GetCourseOfferingsControlStatisticsResponse>>> Handle(
        GetCourseOfferingsControlStatisticsQuery request,
        CancellationToken cancellationToken)
    {
        var data = await _unitOfWork.Repository<Level>()
            .GetQueryable()
            .AsNoTracking()
            .Where(l => l.AcademicProgramId == request.ProgramId
                && l.CourseOfferings.Any(co => co.SemesterId == request.SemesterId)
                && !l.IsDeleted)
            .Select(l => new GetCourseOfferingsControlStatisticsResponse
            (
                l.Id,
                l.Name,
                l.CourseOfferings
                    .Where(co => co.SemesterId == request.SemesterId && !co.IsDeleted)
                    .Select(co => new CourseOfferingStatisticsResponse(
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
                                           && !sa.degree.HasValue)),

                        co.IsOpenForControl
                    )).ToList()
            ))
            .ToListAsync(cancellationToken);

        return Result.Success(data);
    }
}
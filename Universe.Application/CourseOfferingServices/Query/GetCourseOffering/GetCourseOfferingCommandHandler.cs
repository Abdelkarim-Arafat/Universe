using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.CourseOfferingServices.Dtos;

namespace Universe.Application.CourseOfferingServices.Query.GetCourseOffering;

public class GetCourseOfferingCommandHandler (
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetCourseOfferingCommand , Result<CourseOfferingWithDetailsResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Result<CourseOfferingWithDetailsResponse>> Handle(GetCourseOfferingCommand request, CancellationToken cancellationToken)
    {
        var response = await _unitOfWork.Repository<CourseOffering>()
            .GetQueryable()
            .AsNoTracking()
            .Where(x => x.Id == request.Id && !x.IsDeleted)
            .Select(x => new CourseOfferingWithDetailsResponse(
                x.Id,
                x.NumberOfGroups,
                x.CreditHours,
                x.TotalGrade,
                x.SuccessPercentage,
                x.IsOptional,
                x.OptionalGroupCode,
                x.IsIncludedInGpa,
                x.Type,
                x.Semester.Name,
                x.CourseId,
                x.SemesterId,
                x.AcademicProgramId,
                x.LevelId,
                x.Assessments
                    .Select(a => new CourseOfferingAssessmentResponse(
                        a.Id,
                        a.Type,
                        a.MaxScore
                    ))
                    .ToList()
            ))
            .FirstOrDefaultAsync(cancellationToken);

        return response is null
            ? Result.Failure<CourseOfferingWithDetailsResponse>(CourseOfferingErrors.NotFound)
            : Result.Success(response);
    }
}

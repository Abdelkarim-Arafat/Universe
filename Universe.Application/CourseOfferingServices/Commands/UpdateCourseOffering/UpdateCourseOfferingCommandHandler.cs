using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.CourseOffering;

namespace Universe.Application.CourseOfferingServices.Commands.UpdateCourseOffering;

internal class UpdateCourseOfferingCommandHandler (
    IUnitOfWork unitOfWork,
    ICacheService cacheService
) : IRequestHandler<UpdateCourseOfferingCommand, Result<CourseOfferingWithDetailsResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result<CourseOfferingWithDetailsResponse>> Handle(UpdateCourseOfferingCommand request, CancellationToken cancellationToken)
    {
        if ((await _unitOfWork.CourseOfferingRepository
            .GetByIdIncludingAssessmentsAsync(request.Id, cancellationToken)) is not { } course)
            return Result.Failure<CourseOfferingWithDetailsResponse>(CourseOfferingErrors.NotFound);

        if (!(await _unitOfWork.AcademicProgramRepository
            .IsExistAsync(request.AcademicProgramId, cancellationToken)))
            return Result.Failure<CourseOfferingWithDetailsResponse>(AcademicProgramErrors.NotFound);

        if (await _unitOfWork.AcademicYearRepository
            .GetSemesterByTypeAsync(request.AcademicYearId, request.SemesterType, cancellationToken) is not { } semester
            ) return Result.Failure<CourseOfferingWithDetailsResponse>(AcademicYearErrors.NotFound);

        request.Adapt(course);
        course.SemesterId = semester.Id;

        var requestTypes = request.Assessments.ToDictionary(x => x.Type);

        var currentAssessments = course.Assessments;

        foreach (var assessment in currentAssessments)
        {
            if (requestTypes.TryGetValue(assessment.Type, out var req))
            {
                assessment.MaxScore = req.MaxScore;
            }
            else _unitOfWork.Repository<CourseOfferingAssessment>().SoftDelete(assessment);
        }

        var existingTypes = currentAssessments.Select(a => a.Type).ToHashSet();

        var newAssessments = request.Assessments
            .Where(r => !existingTypes.Contains(r.Type))
            .Select(r => new CourseOfferingAssessment
            {
                Type = r.Type,
                MaxScore = r.MaxScore,
                CourseOfferingId = course.Id
            });

        
        await _unitOfWork.Repository<CourseOfferingAssessment>()
            .AddRangeAsync(newAssessments, cancellationToken);

        await _unitOfWork.CompleteAsync(cancellationToken);

        
        await _cacheService.RemoveAsync(CourseOfferingCacheKeys.ById(course.Id), cancellationToken);
        await _cacheService.RemoveAsync(CourseOfferingCacheKeys.LevelCourses(course.LevelId, course.Id), cancellationToken);
        await _cacheService.RemoveByTagAsync(CourseOfferingCacheKeys.Tags(request.AcademicProgramId), cancellationToken);

        var response = (course).Adapt<CourseOfferingWithDetailsResponse>();

        return Result.Success(response);
    }
}
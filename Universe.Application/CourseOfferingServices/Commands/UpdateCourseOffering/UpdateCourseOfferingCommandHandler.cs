
using Universe.Core.Contracts.CourseOfferings;

namespace Universe.Application.CourseOfferingServices.Commands.UpdateCourseOffering;

internal class UpdateCourseOfferingCommandHandler (
    IUnitOfWork unitOfWork
) : IRequestHandler<UpdateCourseOfferingCommand, Result<CourseOfferingWithDetailsResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<CourseOfferingWithDetailsResponse>> Handle(UpdateCourseOfferingCommand request, CancellationToken cancellationToken)
    {
        if ((await _unitOfWork.CourseOfferingRepository
            .GetByIdAsync(request.Id, cancellationToken)) is not { } course)
            return Result.Failure<CourseOfferingWithDetailsResponse>(CourseOfferingErrors.NotFound);

        if (!(await _unitOfWork.AcademicProgramRepository
            .IsExistAsync(request.AcademicProgramId, cancellationToken)))
            return Result.Failure<CourseOfferingWithDetailsResponse>(AcademicProgramErrors.NotFound);

        if (await _unitOfWork.AcademicYearRepository
            .GetSemesterByTypeAsync(request.AcademicYearId, request.SemesterType, cancellationToken) is not { } semester
            ) return Result.Failure<CourseOfferingWithDetailsResponse>(AcademicYearErrors.NotFound);

        request.Adapt(course);
        course.SemesterId = semester.Id;

        var requestTypes = request.Assessments.Select(x => x.Type);

        var currentAssessments = await _unitOfWork.CourseOfferingRepository
            .GetCourseOfferingAssessments(course.Id , cancellationToken);

        foreach (var assessment in currentAssessments)
        {
            if (requestTypes.Contains(assessment.Type))
            {
                var req = request.Assessments.First(r => r.Type == assessment.Type);
                assessment.MaxScore = req.MaxScore;
                _unitOfWork.Repository<CourseOfferingAssessment>().Update(assessment);
            }
            else _unitOfWork.Repository<CourseOfferingAssessment>().SoftDelete(assessment);
        }

        var existingTypes = currentAssessments.Select(a => a.Type);

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

        var courseEntity = await _unitOfWork.Repository<CourseOffering>()
            .GetQueryable()
            .AsNoTracking()
            .Include(c => c.Assessments)
            .Where(x => x.Id == course.Id)
            .FirstOrDefaultAsync(cancellationToken);

        // update(course)
        // حذف الريكويست
        var response = (courseEntity).Adapt<CourseOfferingWithDetailsResponse>();

        return Result.Success(response!);
    }
}
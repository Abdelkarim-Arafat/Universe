using Universe.Core.Contracts.CourseOffering;

namespace Universe.Application.CourseOfferingServices.Commands.AddCourseOffering;

internal class AddCourseOfferingCommandHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService
    ) : IRequestHandler<AddCourseOfferingCommand, Result<CourseOfferingWithDetailsResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result<CourseOfferingWithDetailsResponse>> Handle(AddCourseOfferingCommand request, CancellationToken cancellationToken)
    {
        if (!(await _unitOfWork.AcademicProgramRepository
            .IsExistAsync(request.AcademicProgramId, cancellationToken))
            ) return Result.Failure<CourseOfferingWithDetailsResponse>(AcademicProgramErrors.NotFound);

        if (await _unitOfWork.AcademicYearRepository
            .GetSemesterByTypeAsync(request.AcademicYearId, request.SemesterType, cancellationToken) is not { } semester
            ) return Result.Failure<CourseOfferingWithDetailsResponse>(AcademicYearErrors.NotFound);

        if (await _unitOfWork.CourseOfferingRepository
            .IsExistAsync(request.AcademicProgramId, semester.Id,
                request.LevelId, request.CourseId, cancellationToken)
            ) return Result.Failure<CourseOfferingWithDetailsResponse>(CourseOfferingErrors.AlreadyExist);

        
        if(await _unitOfWork.CourseRepository
            .GetByIdAsync(request.CourseId , cancellationToken) is null) 
            return Result.Failure<CourseOfferingWithDetailsResponse>(CourseErrors.CourseNotFound);

        
        if (await _unitOfWork.LevelRepository
            .GetByIdAsync(request.LevelId, cancellationToken) is null)
            return Result.Failure<CourseOfferingWithDetailsResponse>(LevelErrors.NotFound);

        if (await _unitOfWork.AcademicYearRepository
            .IsExistSemesterAsync(semester.Id , cancellationToken) is false)
            return Result.Failure<CourseOfferingWithDetailsResponse>(SemesterErrors.NotFound);

        var courseOffering = request.Adapt<CourseOffering>();

        courseOffering.SemesterId = semester.Id;

        await _unitOfWork.Repository<CourseOffering>().AddAsync(courseOffering, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);

        await _cacheService.RemoveAsync(CourseOfferingCacheKeys.LevelCourses(courseOffering.LevelId, courseOffering.Id), cancellationToken);
        await _cacheService.RemoveByTagAsync(CourseOfferingCacheKeys.Tags(request.AcademicProgramId) , cancellationToken);

        var response = (courseOffering).Adapt<CourseOfferingWithDetailsResponse>();
        return Result.Success(response);
    }
}

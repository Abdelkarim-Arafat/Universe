
namespace Universe.Application.ExamServices.ExamTermServices.Commands.Create;

public class CreateExamTermCommandHandler
    (IUnitOfWork unitOfWork,
     ICacheService cacheService) : IRequestHandler<CreateExamTermCommand, Result<ExamTermResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result<ExamTermResponse>> Handle(CreateExamTermCommand request, CancellationToken cancellationToken)
    {
        var IsSemesterExist = await _unitOfWork.AcademicYearRepository
            .IsExistSemesterAsync(request.SemesterId, cancellationToken);

        if (!IsSemesterExist)
            return Result.Failure<ExamTermResponse>(SemesterErrors.NotFound);

        var IsProgramExists = await _unitOfWork.AcademicProgramRepository
            .IsExistAsync(request.AcademicProgramId, cancellationToken);

        if (!IsProgramExists)
            return Result.Failure<ExamTermResponse>(AcademicProgramErrors.NotFound);

        var IsExistExamTermWithOverLabedTime = await _unitOfWork.ExamRepository
            .IsExistExamTermWithOverLabedTimeAsync
            (null, request.SemesterId, request.AcademicProgramId,
            request.StartDate, request.EndDate, cancellationToken);

        if (IsExistExamTermWithOverLabedTime)
            return Result.Failure<ExamTermResponse>(ExamErrors.OverlappingTime);

        var IsExistExamTermWithSameType = await _unitOfWork.ExamRepository.IsExistExamTermWithSameTypeAsync
             (null, request.SemesterId, request.AcademicProgramId, request.ExamType, cancellationToken);

        if (IsExistExamTermWithSameType) 
            return Result.Failure<ExamTermResponse>(ExamErrors.ExamTermWithSameType);

        var examTerm = request.Adapt<ExamTerm>();

        await _unitOfWork.Repository<ExamTerm>().AddAsync(examTerm, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);

        var tags = ExamTermCacheKeys.Tags(request.AcademicProgramId);
        await _cacheService.RemoveByTagAsync(tags, cancellationToken);

        var response = new ExamTermResponse
        (
            examTerm.Id,
            examTerm.ExamType,
            examTerm.StartDate,
            examTerm.EndDate,
            examTerm.IsPublished
        );

        return Result.Success(response);
    }
}
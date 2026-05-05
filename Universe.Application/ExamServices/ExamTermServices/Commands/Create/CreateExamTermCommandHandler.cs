using Universe.Application.ExamServices.ExamTermServices.Dtos;

namespace Universe.Application.ExamServices.ExamTermServices.Commands.Create;

public class CreateExamTermCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateExamTermCommand, Result<ExamTermResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

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

        var response = examTerm.Adapt<ExamTermResponse>();

        return Result.Success(response);
    }
}
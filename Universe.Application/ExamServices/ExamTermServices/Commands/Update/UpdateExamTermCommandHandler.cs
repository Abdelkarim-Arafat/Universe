using Universe.Application.ExamServices.ExamTermServices.Dtos;

namespace Universe.Application.ExamServices.ExamTermServices.Commands.Update;

public class UpdateExamTermCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateExamTermCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(UpdateExamTermCommand request, CancellationToken cancellationToken)
    {
        var examTerm = await _unitOfWork.ExamRepository.GetExamTermByIdAsync(request.Id, cancellationToken);

        if (examTerm == null)
            return Result.Failure<ExamTermResponse>(ExamErrors.ExamTermNotFound);

        var IsExistExamTermWithOverLabedTime = await _unitOfWork.ExamRepository
           .IsExistExamTermWithOverLabedTimeAsync
           (examTerm.Id, examTerm.SemesterId, examTerm.AcademicProgramId,
            examTerm.StartDate, examTerm.EndDate, cancellationToken);

        if (IsExistExamTermWithOverLabedTime)
            return Result.Failure<ExamTermResponse>(ExamErrors.OverlabbingTime);

        var IsExistExamTermWithSameType = await _unitOfWork.ExamRepository.IsExistExamTermWithSameTypeAsync
            (null, examTerm.SemesterId, examTerm.AcademicProgramId, examTerm.ExamType, cancellationToken);

        if (IsExistExamTermWithSameType)
            return Result.Failure<ExamTermResponse>(ExamErrors.ExamTermWithSameType);

        examTerm.ExamType = request.ExamType;
        examTerm.StartDate = request.StartDate;
        examTerm.EndDate = request.EndDate;


        _unitOfWork.Repository<ExamTerm>().Update(examTerm);
        await _unitOfWork.CompleteAsync(cancellationToken);

        return Result.Success();
    }
}
using Universe.Application.ExamTermServices.Dtos;

namespace Universe.Application.ExamTermServices.Commands.Delete;

public class DeleteExamTermCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteExamTermCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(DeleteExamTermCommand request, CancellationToken cancellationToken)
    {
        var examTerm = await _unitOfWork.ExamRepository.GetExamTermByIdAsync(request.Id, cancellationToken);
        if (examTerm == null)
            return Result.Failure<ExamTermResponse>(ExamErrors.ExamTermNotFound);

        _unitOfWork.Repository<ExamTerm>().SoftDelete(examTerm);
        await _unitOfWork.CompleteAsync(cancellationToken);

        return Result.Success();
    }
}
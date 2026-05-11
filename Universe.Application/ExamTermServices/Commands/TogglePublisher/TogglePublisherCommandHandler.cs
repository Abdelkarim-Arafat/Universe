using Universe.Application.ExamTermServices.Dtos;

namespace Universe.Application.ExamTermServices.Commands.TogglePublisher;

public class TogglePublisherCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<TogglePublisherCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(TogglePublisherCommand request, CancellationToken cancellationToken)
    {
        var examTerm = await _unitOfWork.ExamRepository.GetExamTermByIdAsync(request.Id, cancellationToken);
        if (examTerm == null)
            return Result.Failure<ExamTermResponse>(ExamErrors.ExamTermNotFound);

        examTerm.IsPublished = !examTerm.IsPublished;

        _unitOfWork.Repository<ExamTerm>().Update(examTerm);
        await _unitOfWork.CompleteAsync(cancellationToken);

        return Result.Success();
    }
}

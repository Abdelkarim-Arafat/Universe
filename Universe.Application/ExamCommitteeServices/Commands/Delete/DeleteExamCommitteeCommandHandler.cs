namespace Universe.Application.ExamCommitteeServices.Commands.Delete;

public class DeleteExamCommitteeCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteExamCommitteeCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(DeleteExamCommitteeCommand request, CancellationToken cancellationToken)
    {
        var examCommittee = await _unitOfWork.ExamRepository.GetExamCommitteeByIdAsync(request.Id, cancellationToken);

        if (examCommittee == null)
            return Result.Failure(ExamErrors.ExamCommitteeNotFound);

        _unitOfWork.Repository<ExamCommittee>().SoftDelete(examCommittee);
        await _unitOfWork.CompleteAsync(cancellationToken);

        return Result.Success();
    }
}
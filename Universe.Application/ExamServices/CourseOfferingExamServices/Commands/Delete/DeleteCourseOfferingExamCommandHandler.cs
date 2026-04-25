namespace Universe.Application.CourseOfferingExamServices.Commands.Delete;

public class DeleteCourseOfferingExamCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteCourseOfferingExamCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(DeleteCourseOfferingExamCommand request, CancellationToken cancellationToken)
    {
        var courseOfferingExam = await _unitOfWork.ExamRepository
            .GetCourseOfferingExamByIdAsync(request.Id, cancellationToken);

        if (courseOfferingExam == null)
            return Result.Failure(ExamErrors.CourseOfferingExamNotFound);

        _unitOfWork.Repository<CourseOfferingExam>().DeletePermanently(courseOfferingExam);
        await _unitOfWork.CompleteAsync(cancellationToken);

        return Result.Success();
    }
}
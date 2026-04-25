namespace Universe.Application.CourseOfferingExamServices.Commands.Update;

public class UpdateCourseOfferingExamCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateCourseOfferingExamCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(UpdateCourseOfferingExamCommand request, CancellationToken cancellationToken)
    {
        var courseOfferingExam = await _unitOfWork.ExamRepository
           .GetCourseOfferingExamByIdAsync(request.Id, cancellationToken);

        if (courseOfferingExam == null)
            return Result.Failure(ExamErrors.CourseOfferingExamNotFound);

        courseOfferingExam.Date = request.Date;
        courseOfferingExam.StartTime = request.StartTime;
        courseOfferingExam.EndTime = request.EndTime;

        _unitOfWork.Repository<CourseOfferingExam>().Update(courseOfferingExam);
        await _unitOfWork.CompleteAsync(cancellationToken);

        return Result.Success(courseOfferingExam);
    }
}
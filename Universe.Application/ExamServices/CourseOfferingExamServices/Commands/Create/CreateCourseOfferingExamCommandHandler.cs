using Universe.Application.CourseOfferingExamServices.Dtos;

namespace Universe.Application.CourseOfferingExamServices.Commands.Create;

public class CreateCourseOfferingExamCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<CreateCourseOfferingExamCommand, Result<CourseOfferingExamResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<CourseOfferingExamResponse>> Handle(CreateCourseOfferingExamCommand request, CancellationToken cancellationToken)
    {
         var isCourseOfferingExist = await _unitOfWork.CourseOfferingRepository
            .IsExistAsync(request.CourseOfferingId, cancellationToken);

        if (!isCourseOfferingExist)
            return Result.Failure<CourseOfferingExamResponse>(CourseOfferingErrors.NotFound);

        var isExamTermExist = await _unitOfWork.ExamRepository
            .IsExistExamTermAsync(request.ExamTermId, cancellationToken);

        if (!isExamTermExist)
            return Result.Failure<CourseOfferingExamResponse>(ExamErrors.ExamTermNotFound);

        var courseOfferingExam = request.Adapt<CourseOfferingExam>();

        await _unitOfWork.Repository<CourseOfferingExam>().AddAsync(courseOfferingExam, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);

        return Result.Success(courseOfferingExam.Adapt<CourseOfferingExamResponse>());
    }
}
using Universe.Application.UserServices.UserDtos;

namespace Universe.Application.UserServices.Querys.GetPreviousQualificationData;


public class GetPreviousQualificationDataCommandHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetPreviousQualificationDataCommand, Result<PreviousQualificationResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<PreviousQualificationResponse>> Handle(GetPreviousQualificationDataCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.UserRepository.GetStudentByIdAsync(request.StudentId , cancellationToken)
            is not { } student) return Result.Failure<PreviousQualificationResponse>(StudentErrors.UserNotFound);

        return Result.Success(student.PreviousQualification.Adapt<PreviousQualificationResponse>());
    }
}
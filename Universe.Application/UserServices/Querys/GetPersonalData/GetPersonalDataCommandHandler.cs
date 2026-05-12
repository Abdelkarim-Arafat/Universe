using Universe.Application.UserServices.UserDtos;

namespace Universe.Application.UserServices.Querys.GetPersonalData;

public class GetPersonalDataCommandHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetPersonalDataCommand , Result<PersonalDataResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<PersonalDataResponse>> Handle(GetPersonalDataCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.UserRepository.GetStudentByIdAsync(request.StudentId , cancellationToken)
            is not { } student) return Result.Failure<PersonalDataResponse>(StudentErrors.NotFound);

        return Result.Success(student.Adapt<PersonalDataResponse>());
    }
}

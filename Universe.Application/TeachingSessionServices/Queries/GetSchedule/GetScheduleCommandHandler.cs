
using Universe.Application.TeachingSessionServices.SessionDtos;

namespace Universe.Application.TeachingSessionServices.Queries.GetSchedule;

public class GetScheduleCommandHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetScheduleCommand , Result<ScheduleResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<ScheduleResponse>> Handle(GetScheduleCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.AcademicProgramRepository
            .GetScheduleAsync(request.ProgramId, request.SemesterId, cancellationToken)
            is not { } schedule) return Result.Failure<ScheduleResponse>(AcademicProgramErrors.ScheduleNotFound);

        return Result.Success(schedule.Adapt<ScheduleResponse>());
    }
}

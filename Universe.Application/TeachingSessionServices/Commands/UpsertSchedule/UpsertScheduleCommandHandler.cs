using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.TeachingSessionServices.Commands.UpsertSchedule;
using Universe.Core.Contracts.TeachingSession;

namespace Universe.Application.TeachingSessionServices.Commands.UpsertSchedule;

public class UpsertScheduleCommandHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService
    ) : IRequestHandler<UpsertScheduleCommand, Result<ScheduleResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result<ScheduleResponse>> Handle(UpsertScheduleCommand request, CancellationToken cancellationToken)
    {
        if (!(await _unitOfWork.AcademicProgramRepository
            .IsExistAsync(request.ProgramId, cancellationToken))
            ) return Result.Failure<ScheduleResponse>(AcademicProgramErrors.NotFound);

        if (!(await _unitOfWork.AcademicYearRepository
            .IsExistSemesterAsync(request.SemesterId, cancellationToken))
            ) return Result.Failure<ScheduleResponse>(SemesterErrors.NotFound);

        if (await _unitOfWork.SessionRepository
            .HasSessionsAsync(request.ProgramId, request.SemesterId, cancellationToken)
            ) return Result.Failure<ScheduleResponse>(SessionErrors.ExistingSessions);

        var schedule = await _unitOfWork.AcademicProgramRepository
            .GetScheduleAsync(request.ProgramId, request.SemesterId, cancellationToken);

        if (schedule is null)
        {
            schedule = new ProgramSchedule
            {
                ProgramId = request.ProgramId,
                SemesterId = request.SemesterId,
                DayStartTime = request.DayStartTime,
                DayEndTime = request.DayEndTime,
                SlotDurationMinutes = request.SlotDurationMinutes
            };

            await _unitOfWork.Repository<ProgramSchedule>().AddAsync(schedule, cancellationToken);
        }
        else
        {
            request.Adapt(schedule);
            _unitOfWork.Repository<ProgramSchedule>().Update(schedule);
        }

        await _unitOfWork.CompleteAsync(cancellationToken);

        await _cacheService.RemoveAsync(SessionCacheKeys.Schedule(request.ProgramId, request.SemesterId), cancellationToken);

        return Result.Success(schedule.Adapt<ScheduleResponse>());
    }
}


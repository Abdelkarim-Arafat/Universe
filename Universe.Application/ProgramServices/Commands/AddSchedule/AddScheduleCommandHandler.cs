using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.ProgramServices.ProgramDtos;

namespace Universe.Application.ProgramServices.Commands.AddSchedule;

public class AddScheduleCommandHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<AddScheduleCommand, Result<ScheduleResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<ScheduleResponse>> Handle(AddScheduleCommand request, CancellationToken cancellationToken)
    {
        if(!await _unitOfWork.AcademicProgramRepository
            .IsExistAsync(request.ProgramId, cancellationToken)
            ) return Result.Failure<ScheduleResponse>(AcademicProgramErrors.AcademicProgramNotFound);

        if (!await _unitOfWork.AcademicYearRepository
            .IsExistSemesterAsync(request.ProgramId, cancellationToken)
            ) return Result.Failure<ScheduleResponse>(SemesterErrors.NotFound);

        if (await _unitOfWork.AcademicProgramRepository
            .GetScheduleAsync(request.ProgramId, request.SemesterId, cancellationToken)
            is not null) return Result.Failure<ScheduleResponse>(AcademicProgramErrors.ScheduleAlreadyExist);

        var schedule = request.Adapt<ProgramSchedule>();

        await _unitOfWork.Repository<ProgramSchedule>().AddAsync(schedule, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);

        return Result.Success(schedule.Adapt<ScheduleResponse>());
    }
}

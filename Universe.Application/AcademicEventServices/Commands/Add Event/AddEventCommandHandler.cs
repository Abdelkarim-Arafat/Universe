using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.AcademicEventServices.EvenetDtos;

namespace Universe.Application.AcademicEventServices.Commands.Add_Event;

internal class AddEventCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddEventCommand, Result<EventResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<EventResponse>> Handle(AddEventCommand request, CancellationToken cancellationToken)
    {
        if (!await _unitOfWork.AcademicProgramRepository
            .IsExistAsync(request.ProgramId, cancellationToken)
            ) return Result.Failure<EventResponse>(AcademicProgramErrors.AcademicProgramNotFound);

        if(!await _unitOfWork.AcademicYearRepository
            .IsExistSemesterAsync(request.SemesterId , cancellationToken)
            ) return Result.Failure<EventResponse>(SemesterErrors.NotFound);

        if(await _unitOfWork.AcademicEventRepository
            .IsOverlabedAsync(request.ProgramId, request.SemesterId, request.Type,
                request.StartDate, request.EndDate, cancellationToken)
            ) return Result.Failure<EventResponse>(AcademicEventErrors.OverLabedDateTime);

        var academicEvent = new AcademicEvent
        {
            ProgramId = request.ProgramId,
            SemesterId = request.SemesterId,
            Type = request.Type,
            StartDate = request.StartDate,
            EndDate = request.EndDate
        };

        await _unitOfWork.Repository<AcademicEvent>().AddAsync(academicEvent, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);

        return Result.Success(academicEvent.Adapt<EventResponse>());
    }
}

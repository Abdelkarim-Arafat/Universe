using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.AcademicEventServices.Commands.Remove_Event;

public class RemoveAcademicEventCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<RemoveAcademicEventCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(RemoveAcademicEventCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.AcademicEventRepository
            .GetByProgramAndSemesterIdsAsync(request.ProgramId, request.SemesterId, request.Type, cancellationToken)
            is not { } academicEvent) return Result.Failure(AcademicEventErrors.NotFound);
        
        _unitOfWork.Repository<AcademicEvent>().DeletePermanently(academicEvent);
        await _unitOfWork.CompleteAsync(cancellationToken);

        return Result.Success();
    }
}

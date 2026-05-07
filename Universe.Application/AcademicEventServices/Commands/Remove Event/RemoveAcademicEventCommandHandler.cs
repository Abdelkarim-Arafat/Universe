using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.AcademicEventServices.Commands.Remove_Event;

public class RemoveAcademicEventCommandHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService
    ) : IRequestHandler<RemoveAcademicEventCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result> Handle(RemoveAcademicEventCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.AcademicEventRepository
            .GetByIdAsync(request.Id, cancellationToken)
            is not { } academicEvent) return Result.Failure(AcademicEventErrors.NotFound);

        await _cacheService.RemoveByTagAsync(AcademicEventCacheKeys.Tags(academicEvent.ProgramId, academicEvent.SemesterId), cancellationToken);
        
        _unitOfWork.Repository<AcademicEvent>().DeletePermanently(academicEvent);
        await _unitOfWork.CompleteAsync(cancellationToken);

        return Result.Success();
    }
}

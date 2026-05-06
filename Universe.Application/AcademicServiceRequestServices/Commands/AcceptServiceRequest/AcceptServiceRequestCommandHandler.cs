using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Enums;

namespace Universe.Application.AcademicServiceRequestServices.Commands.AcceptServiceRequest;

public class AcceptServiceRequestCommandHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService
    ) : IRequestHandler<AcceptServiceRequestCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result> Handle(AcceptServiceRequestCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.ServiceRepository
            .GetRequestByIdAsync(request.RequestId, cancellationToken) is not { } serviceRequest
            ) return Result.Failure(ServiceErrors.RequestNotFound);

        serviceRequest.Status = RequestStatus.Ready;
        serviceRequest.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.Repository<ServiceRequest>().Update(serviceRequest);
        await _unitOfWork.CompleteAsync(cancellationToken);

        await _cacheService.RemoveByTagAsync(ServiceRequestCacheKeys.Tags(request.CollegeId) , cancellationToken);

        return Result.Success();
    }
}

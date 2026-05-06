using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.Service;

namespace Universe.Application.AcademicServiceServices.Commands.UpdateService;

public class UpdateServiceCommandHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService
    ) : IRequestHandler<UpdateServiceCommand, Result<ServiceResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result<ServiceResponse>> Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
    {
        if(await _unitOfWork.ServiceRepository
            .GetByIdAsync(request.Id, cancellationToken) is not { } service
            ) return Result.Failure<ServiceResponse>(ServiceErrors.NotFound);
            
        request.Adapt(service);

        await _unitOfWork.CompleteAsync(cancellationToken);

        await _cacheService.RemoveAsync(ServiceCacheKeys.ById(request.Id), cancellationToken);
        await _cacheService.RemoveByTagAsync(ServiceCacheKeys.Tags(request.CollegeId), cancellationToken);

        var response = service.Adapt<ServiceResponse>();
        return Result.Success(response);
    }
}


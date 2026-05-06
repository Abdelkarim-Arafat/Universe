using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.Service;

namespace Universe.Application.AcademicServiceServices.Commands.RemoveService;

public class RemoveServiceCommandHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService
    ) : IRequestHandler<RemoveServiceCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(RemoveServiceCommand request, CancellationToken cancellationToken)
    {
        if(await _unitOfWork.ServiceRepository
            .GetByIdAsync(request.Id, cancellationToken) is not { } service
            ) return Result.Failure<ServiceResponse>(ServiceErrors.NotFound);

        _unitOfWork.Repository<Service>().SoftDelete(service);
        await _unitOfWork.CompleteAsync(cancellationToken);
        
        await _cacheService.RemoveAsync(ServiceCacheKeys.ById(request.Id), cancellationToken);
        await _cacheService.RemoveByTagAsync(ServiceCacheKeys.Tags(service.CollegeId), cancellationToken);

        return Result.Success();
    }
}

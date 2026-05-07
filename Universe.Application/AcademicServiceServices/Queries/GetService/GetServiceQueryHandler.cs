using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.AcademicProgramServices.Query.GetAcademicProgram;
using Universe.Core.Contracts.AcademicProgram;
using Universe.Core.Contracts.Service;

namespace Universe.Application.AcademicServiceServices.Queries.GetService;

public class GetServiceQueryHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService
    ) : IRequestHandler<GetServiceQuery, Result<ServiceResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<ServiceResponse>> Handle(GetServiceQuery request, CancellationToken cancellationToken)
    {
        var response = await _cacheService.GetOrCreateAsync(
            key: ServiceCacheKeys.ById(request.ServiceId),
            factory: async () => {
                var service = await _unitOfWork.ServiceRepository
                    .GetByIdAsync(request.ServiceId, cancellationToken);

                return service.Adapt<ServiceResponse>();
            },
            cancellationToken: cancellationToken
        );

        if (response is null)
            return Result.Failure<ServiceResponse>(ServiceErrors.NotFound);

        return Result.Success(response);
    }
}
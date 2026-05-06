using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.Service;

namespace Universe.Application.AcademicServiceServices.Commands.Add_Service;

internal class AddServiceCommandHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService
    ) : IRequestHandler<AddServiceCommand , Result<ServiceResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result<ServiceResponse>> Handle(AddServiceCommand request, CancellationToken cancellationToken)
    {
        if(await _unitOfWork.CollegeRepository
            .IsExistAsync(request.CollegeId, cancellationToken) is false
            ) return Result.Failure<ServiceResponse>(CollegeErrors.NotFound);

        var service = request.Adapt<Service>();

        await _unitOfWork.Repository<Service>().AddAsync(service, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);

        return Result.Success(service.Adapt<ServiceResponse>());
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.AcademicServiceServices.ServiceDtos;

namespace Universe.Application.AcademicServiceServices.Commands.UpdateService;

public class UpdateServiceCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateServiceCommand, Result<ServiceResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<ServiceResponse>> Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.ServiceRepository
            .GetByIdAsync(request.Id, cancellationToken) is not { } service
            ) return Result.Failure<ServiceResponse>(ServiceErrors.NotFound);

        request.Adapt(service);

        _unitOfWork.Repository<Service>().Update(service);
        await _unitOfWork.CompleteAsync(cancellationToken);

        return Result.Success(service.Adapt<ServiceResponse>());
    }
}


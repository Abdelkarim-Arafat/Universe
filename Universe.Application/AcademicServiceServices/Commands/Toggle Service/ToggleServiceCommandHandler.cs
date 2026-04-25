using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.AcademicServiceServices.Commands.Toggle_Service;

internal class ToggleServiceCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<ToggleServiceCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Result> Handle(ToggleServiceCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.ServiceRepository
            .GetByIdAsync(request.Id, cancellationToken) is not { } service
            ) return Result.Failure<ServiceResponse>(ServiceErrors.NotFound);

        service.IsActive = !service.IsActive;

        _unitOfWork.Repository<Service>().Update(service);
        await _unitOfWork.CompleteAsync(cancellationToken);

        return Result.Success();
    }

}

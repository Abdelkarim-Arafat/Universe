using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.AcademicServiceServices.Commands.RemoveService;

public class RemoveServiceCommandHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<RemoveServiceCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Result> Handle(RemoveServiceCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.ServiceRepository
            .GetByIdAsync(request.Id, cancellationToken) is not { } service
            ) return Result.Failure<ServiceResponse>(ServiceErrors.NotFound);

        _unitOfWork.Repository<Service>().SoftDelete(service);
        await _unitOfWork.CompleteAsync(cancellationToken);

        return Result.Success();
    }
}

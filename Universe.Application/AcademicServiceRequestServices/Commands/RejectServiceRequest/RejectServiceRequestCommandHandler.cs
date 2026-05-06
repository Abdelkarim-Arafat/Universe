using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Enums;

namespace Universe.Application.AcademicServiceRequestServices.Commands.RejectServiceRequest;

internal class RejectServiceRequestCommandHandler(
    IUnitOfWork unitOfWork,
    IPayPalService paypal,
    ICacheService cacheService
    ) : IRequestHandler<RejectServiceRequestCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IPayPalService _paypal = paypal;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result> Handle(RejectServiceRequestCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.ServiceRepository
            .GetRequestByIdAsync(request.RequestId, cancellationToken) is not { } serviceRequest
            ) return Result.Failure(ServiceErrors.RequestNotFound);

        var payment = await _unitOfWork.PaymentRepository
            .GetByIdAsync(serviceRequest.PaymentId , cancellationToken);

        if (await _paypal.RefundPaymentAsync(payment.OrderId) is false
            ) return Result.Failure(PaymentErrors.FaildRefund);

        payment.Status = PaymentStatus.Refunded;
        serviceRequest.Status = RequestStatus.Rejected;
        serviceRequest.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.CompleteAsync(cancellationToken);

        await _cacheService.RemoveByTagAsync(ServiceRequestCacheKeys.Tags(request.CollegeId), cancellationToken);
        
        return Result.Success();
    }
}

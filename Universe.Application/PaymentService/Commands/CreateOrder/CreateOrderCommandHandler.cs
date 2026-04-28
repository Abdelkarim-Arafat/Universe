using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Universe.Core.Contracts.PayPal;
using Universe.Core.Enums;

namespace Universe.Application.PaymentService.Commands.CreateOrder;

public class CreateOrderCommandHandler(
    IUnitOfWork unitOfWork,
    IHttpContextAccessor contextAccessor,
    IPayPalService payPalService
    ) : IRequestHandler<CreateOrderCommand, Result<CreateOrderResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor = contextAccessor;
    private readonly IPayPalService _payPalService = payPalService;

    public async Task<Result<CreateOrderResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var user = _contextAccessor.HttpContext!.User;
        var userId = user.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        if (await _unitOfWork.ServiceRepository
            .GetByIdAsync(request.ServiceId, cancellationToken) is not { } service
            ) return Result.Failure<CreateOrderResponse>(ServiceErrors.NotFound);

        var result = await _payPalService.CreateOrderAsync(service.Price);

        var payment = new Payment
        {
            OrderId = result.Id,
            Price = service.Price,
            StudentId = Guid.Parse(userId!),
            ServiceId = service.Id,
            Status = PaymentStatus.Pending
        };

        await _unitOfWork.Repository<Payment>().AddAsync(payment, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);

        return Result.Success(result);
    }
}

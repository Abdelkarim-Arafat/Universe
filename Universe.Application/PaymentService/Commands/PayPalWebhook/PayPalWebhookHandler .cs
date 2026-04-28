using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Universe.Application.PaymentService.Commands.PayPalWebhook;
using Universe.Core.Enums;

namespace Universe.Application.PaymentService.Commands.PayPalWebhook;

internal class PayPalWebhookHandler (
    IUnitOfWork unitOfWork,
    IPayPalService payPalService
    ) : IRequestHandler<PayPalWebhookCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IPayPalService _payPalService = payPalService;

    public async Task Handle(PayPalWebhookCommand request, CancellationToken cancellationToken)
    {
        var json = JsonDocument.Parse(request.Payload);
        var eventType = json.RootElement.GetProperty("event_type").GetString();
        var resource = json.RootElement.GetProperty("resource");

        string? orderId = null;
        if (eventType == "CHECKOUT.ORDER.APPROVED") orderId = resource.GetProperty("id").GetString();
        else orderId = resource.GetProperty("supplementary_data").GetProperty("related_ids").GetProperty("order_id").GetString();

        if (string.IsNullOrEmpty(orderId)) return;

        if (await _unitOfWork.PaymentRepository
            .GetByOrderIdAsync(orderId, cancellationToken) is not { } payment
            ) return;


        if (payment.Status == PaymentStatus.Completed) return;

        if (eventType == "CHECKOUT.ORDER.APPROVED")
        {
            await _payPalService.CaptureOrderAsync(orderId);
            return;
        }

        if (eventType == "PAYMENT.CAPTURE.COMPLETED")
        {
            var serviceRequest = new ServiceRequest
            {
                StudentId = payment.StudentId,
                ServiceId = payment.ServiceId,
                PaymentId = payment.Id,
                Status = RequestStatus.Pending
            };

            await _unitOfWork.Repository<ServiceRequest>().AddAsync(serviceRequest, cancellationToken);
            payment.Status = PaymentStatus.Completed;
        }
        else if (eventType == "PAYMENT.CAPTURE.CANCELLED")
        {
            payment.Status = PaymentStatus.Cancelled;
        }
        else if (eventType == "PAYMENT.CAPTURE.REFUNDED")
        {
            payment.Status = PaymentStatus.Refunded;
        }
        else payment.Status = PaymentStatus.Failed;

        await _unitOfWork.CompleteAsync(cancellationToken);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.PayPal;

namespace Universe.Core.Interfaces;
public interface IPayPalService
{
    Task<CreateOrderResponse> CreateOrderAsync(decimal amount);
    Task CaptureOrderAsync(string orderId);
    Task<bool> RefundPaymentAsync(string captureId);
}

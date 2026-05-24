using Microsoft.AspNetCore.Http;
using Universe.Core.Abstractions;

namespace Universe.Core.Errors;

public class PaymentErrors
{
    public static readonly Error FaildRefund = new(
       "PaymentRefund.Faild",
       "Refund Faild",
       StatusCodes.Status400BadRequest);
}

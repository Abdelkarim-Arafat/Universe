using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Core.Contracts.PayPal;

public record CreateOrderResponse(
    string Id,
    string ApprovalUrl
);
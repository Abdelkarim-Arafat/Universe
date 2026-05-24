namespace Universe.Core.Contracts.PayPal;

public record CreateOrderResponse(
    string Id,
    string ApprovalUrl
);
using Universe.Core.Entities;

namespace Universe.Core.Interfaces.Repositories;

public interface IPaymentRepository
{
    Task<Payment> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Payment?> GetByOrderIdAsync(string orderId, CancellationToken cancellationToken);
}

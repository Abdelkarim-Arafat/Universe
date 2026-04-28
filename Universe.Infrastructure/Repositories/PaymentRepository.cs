using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities;
using Universe.Core.Interfaces.Repositories;
using Universe.Infrastructure.Persistence;

namespace Universe.Infrastructure.Repositories;

internal class PaymentRepository(ApplicationDbContext context) : IPaymentRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Payment> GetByOrderIdAsync(string orderId, CancellationToken cancellationToken)
        => await _context.Payments.FirstAsync(x => x.OrderId == orderId , cancellationToken);

    public async Task<Payment> GetByIdAsync(Guid id , CancellationToken cancellationToken)
        => await _context.Payments.FirstAsync(x => x.Id == id, cancellationToken);
}

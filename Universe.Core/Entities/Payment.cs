using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities.Core;
using Universe.Core.Enums;

namespace Universe.Core.Entities;

public class Payment : BaseEntity
{
    public Guid Id { get; set; }
    public Payment() { Id = Guid.CreateVersion7(); }
    public string OrderId { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
    public Guid StudentId { get; set; }
    public Student Student { get; set; } = default!;
    public Guid ServiceId { get; set; }
    public Service Service { get; set; } = default!;
}

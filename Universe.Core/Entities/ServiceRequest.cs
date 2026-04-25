using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities.Core;
using Universe.Core.Enums;

namespace Universe.Core.Entities;

public class ServiceRequest : BaseEntity
{
    public Guid Id { get; set; }
    public ServiceRequest() { Id = Guid.CreateVersion7(); }
    public Guid StudentId { get; set; }
    public Student Student { get; set; } = default!;
    public Guid ServiceId { get; set; }
    public Service Service { get; set; } = default!;
    public Guid PaymentId { get; set; }
    public Payment Payment { get; set; } = default!;
    public RequestStatus Status { get; set; } = RequestStatus.Pending;
}

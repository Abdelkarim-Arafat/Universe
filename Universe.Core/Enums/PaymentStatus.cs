using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Core.Enums;

public enum PaymentStatus
{
    Pending = 1,
    Completed = 2,
    Failed = 3,
    Cancelled = 4,
    Refunded = 5
}
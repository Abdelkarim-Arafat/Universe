using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Core.Contracts.Service;

public record ServiceResponse(
    Guid Id,
    string Name,
    string Description,
    decimal Price
);
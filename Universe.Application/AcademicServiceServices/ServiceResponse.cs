using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.AcademicServiceServices;

public record ServiceResponse(
    string Id,
    string Name,
    string Description,
    decimal Price
);
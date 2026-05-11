using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Core.Contracts.User;

public record StuffResponse(
    string Id,
    string Name
);
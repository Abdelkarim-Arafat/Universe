using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Core.Contracts.User;

public record StuffWithDetailsResponse (
    string Id,
    string Name,
    List<string> Roles,
    string UserName,
    string ?Email,
    string ?PhoneNumber
);
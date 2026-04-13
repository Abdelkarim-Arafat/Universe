using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.UserServices.UserDtos;

public record StuffWithDetailsResponse (
    string Id,
    string Name,
    List<string> Roles,
    string UserName,
    string ?Email,
    string ?PhoneNumber
);
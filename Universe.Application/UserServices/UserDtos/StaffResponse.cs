using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.UserServices.UserDtos;

public record StaffResponse (
    string Id,
    string Name,
    List<string> Roles,
    string UserName,
    string ?Email,
    string ?PhoneNumber
);
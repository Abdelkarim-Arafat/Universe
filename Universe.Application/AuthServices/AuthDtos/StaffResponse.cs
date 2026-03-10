using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.AuthServices.AuthDtos;

public record StaffResponse(
    string Id,
    string Name,
    string Role,
    string UserName
);
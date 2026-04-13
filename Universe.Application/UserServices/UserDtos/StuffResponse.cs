using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.UserServices.UserDtos;

public record StuffResponse(
    string Id,
    string Name
);
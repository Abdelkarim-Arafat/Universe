using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.UserServices.UserDtos;

public record StudentResponse(
    Guid Id,
    string Name,
    string StudentCode,
    string NationalId
);
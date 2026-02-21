using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.UserServices.UserDtos;

public record RegisterStudentResponse(
    string Id,
    string Name,
    string UserName
);

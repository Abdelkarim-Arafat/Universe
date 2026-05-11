using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Core.Contracts.User;

public record RegisterStudentResponse(
    string Id,
    string Name,
    string UserName
);

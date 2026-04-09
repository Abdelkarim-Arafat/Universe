using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.UserServices.Commands.RemoveStudent;

public record RemoveStudentCommand(
    [Required] Guid Id
) : IRequest<Result>;

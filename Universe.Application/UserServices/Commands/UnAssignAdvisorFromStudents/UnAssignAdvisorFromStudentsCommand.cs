using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.UserServices.Commands.UnAssignAdvisorFromStudents;

public record UnAssignAdvisorFromStudentsCommand(
    List<Guid> StudentIds
) : IRequest<Result>;
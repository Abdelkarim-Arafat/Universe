using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.UserServices.Commands.AssignAdvisorToStudents;

public record AssignAdvisorToStudentsCommand(
    [Required] Guid AdvisorId,
    [Required] List<Guid> StudentIds
) : IRequest<Result>;
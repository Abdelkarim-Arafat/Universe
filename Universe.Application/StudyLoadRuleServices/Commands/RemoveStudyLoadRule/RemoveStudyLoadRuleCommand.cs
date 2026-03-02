using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.StudyLoadRuleServices.Commands.RemoveStudyLoadRule;

public record RemoveStudyLoadRuleCommand(
    [Required] Guid Id
) : IRequest<Result>;

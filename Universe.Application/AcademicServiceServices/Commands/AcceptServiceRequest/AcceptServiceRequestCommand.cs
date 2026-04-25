using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Enums;

namespace Universe.Application.AcademicServiceServices.Commands.ChangeServiceRequestStatus;

public record AcceptServiceRequestCommand(
    Guid RequestId
) : IRequest<Result>;
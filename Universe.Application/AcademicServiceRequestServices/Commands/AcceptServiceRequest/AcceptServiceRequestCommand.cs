using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Enums;

namespace Universe.Application.AcademicServiceRequestServices.Commands.AcceptServiceRequest;

public record AcceptServiceRequestCommand(
    Guid RequestId
) : IRequest<Result>;
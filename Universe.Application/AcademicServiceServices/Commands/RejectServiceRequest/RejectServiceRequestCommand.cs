using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.AcademicServiceServices.Commands.RejectServiceRequest;

public record RejectServiceRequestCommand(
    [Required] Guid RequestId
) : IRequest<Result>;
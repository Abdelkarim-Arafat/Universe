using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.UserServices.Commands.RemoveStuff;

public record RemoveStuffCommand(
    [Required] Guid Id
) : IRequest<Result>;

using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.UserServices.UserDtos;

namespace Universe.Application.UserServices.Querys.GetParentData;

public record GetParentDataCommand(
    [Required] Guid StudentId
) : IRequest<Result<ParentDataResponse>>;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.User;

namespace Universe.Application.UserServices.Querys.GetParentData;

public record GetParentDataQuery(
    [Required] Guid StudentId
) : IRequest<Result<ParentDataResponse>>;
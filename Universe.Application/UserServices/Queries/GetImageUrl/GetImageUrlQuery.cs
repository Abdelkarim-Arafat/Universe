using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.UserServices.Queries.GetImageUrl;

public record GetImageUrlQuery(
    [Required] Guid UserId
) : IRequest<Result<string?>>;
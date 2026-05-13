using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.User;

namespace Universe.Application.UserServices.Querys.GetPreviousQualificationData;

public record GetPreviousQualificationDataQuery (
    [Required] Guid StudentId
) : IRequest<Result<PreviousQualificationResponse>>;
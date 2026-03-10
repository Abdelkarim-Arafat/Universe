using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.UserServices.UserDtos;

namespace Universe.Application.UserServices.Querys.GetPreviousQualificationData;

public record GetPreviousQualificationDataCommand (
    [Required] Guid StudentId
) : IRequest<Result<PreviousQualificationResponse>>;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.UserServices.UserDtos;
using Universe.Core.Enums;

namespace Universe.Application.UserServices.Commands.UpdateMilitaryData;

public record UpdateMilitaryDataCommand(
    [Required] Guid UserId,
    MilitaryStatus MilitaryStatus, // الموقف من التجنيد
    string MilitaryNumber, // الرقم العسكري
    string DecisionNumber, // رقم قرار التجنيد
    DateOnly DecisionDate, // تاريخ قرار التجنيد
    DateOnly EnrollmentDateault, // تاريخ الالتحاق
    DateOnly EndDate // تاريخ نهاية الالتحاق
) : IRequest<Result<MilitaryDataResponse>>;
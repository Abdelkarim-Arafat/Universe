using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Enums;

namespace Universe.Application.UserServices.UserDtos;

public record MilitaryDataResponse (
    MilitaryStatus? MilitaryStatus, // الموقف من التجنيد
    string MilitaryNumber, // الرقم العسكري
    string DecisionNumber, // رقم قرار التجنيد
    DateOnly? DecisionDate, // تاريخ قرار التجنيد
    DateOnly? EnrollmentDateault, // تاريخ الالتحاق
    DateOnly? EndDate // تاريخ نهاية الالتحاق
);
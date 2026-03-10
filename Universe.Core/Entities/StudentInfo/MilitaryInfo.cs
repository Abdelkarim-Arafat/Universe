using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Universe.Core.Enums;

namespace Universe.Core.Entities.StudentInfo;

[Owned]
public class MilitaryInfo
{
    public MilitaryStatus? MilitaryStatus { get; set; } // الموقف من التجنيد
    public string MilitaryNumber { get; set; } = string.Empty; // الرقم العسكري
    public string DecisionNumber { get; set; } = string.Empty; // رقم قرار التجنيد
    public DateOnly? DecisionDate { get; set; } // تاريخ قرار التجنيد
    public DateOnly? EnrollmentDate { get; set; } // تاريخ الالتحاق
    public DateOnly? EndDate { get; set; } // تاريخ نهاية الالتحاق
}

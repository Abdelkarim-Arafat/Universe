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
    public MilitaryStatus MilitaryStatus { get; set; } = MilitaryStatus.None;// الموقف من التجنيد
    public string MilitaryNumber { get; set; } = string.Empty; // الرقم العسكري
    public string DecisionNumber { get; set; } = string.Empty; // رقم قرار التجنيد
    public DateOnly DecisionDate { get; set; } = default; // تاريخ قرار التجنيد
    public DateOnly EnrollmentDate { get; set; } = default; // تاريخ الالتحاق
    public DateOnly EndDate { get; set; } = default; // تاريخ نهاية الالتحاق
}

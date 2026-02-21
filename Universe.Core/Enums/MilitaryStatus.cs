using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Core.Enums;

public enum MilitaryStatus
{
    None = 0,
    Exempted = 1,      // اعفاء
    Postponed = 2,     // تأجيل
    Completed = 3,     // ادى الخدمة
    Serving = 4,       // بالخدمة
    NotRequired = 5    // غير مطلوب
}
using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Core.Enums;

public enum AssessmentType
{
    FinalExam = 1,        // امتحان نهاية الفصل
    MidtermExam = 2,      // ميدتيرم
    OralExam = 3,         // شفوي
    PracticalExam = 4,    // عملي
    MCQExam = 5,          // اختيار من متعدد
    Project = 6,          // مشروع
    Assignment = 7,       // تكليف
    Quiz = 8,             // كويز
    Attendance = 9,       // حضور
    YearWork = 10         // أعمال سنة
}
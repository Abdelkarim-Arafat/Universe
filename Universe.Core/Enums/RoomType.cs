using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Core.Enums;

public enum RoomType
{
    LectureHall = 1,     // مدرج كبير (للمحاضرات النظرية)
    ClassRoom = 2,       // قاعة تدريس عادية (للسكاشن أو أعداد صغيرة)
    ComputerLab = 3,     // معامل الحاسب الآلي
    ScientificLab = 4,   // معامل العلوم (كيمياء، فيزياء، طب)
    Workshop = 5,        // ورشة عمل (للهندسة، الفنون، النجارة..)
    Studio = 6,          // ستوديو (للإعلام، العمارة، التصميم)
    Clinic = 7,          // عيادة تعليمية (للمجالات الطبية)
    LanguageLab = 8,     // معامل اللغات
    DrawingRoom = 9      // صالة رسم (للهندسة والفنون)
}

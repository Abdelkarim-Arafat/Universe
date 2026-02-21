using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Enums;

namespace Universe.Core.Entities.StudentInfo;

[Owned]
public class PreviousQualification
{
    public string SchoolName { get; set; } = string.Empty; // المدرسة
    public string EnrollmentYear { get; set; } = string.Empty; // سنة الالتحاق
    public int SeatNumber { get; set; } // رقم الجلوس
    public string Qualification { get; set; } = string.Empty; // المؤهل
    public string GraduationYear { get; set; } = string.Empty;// سنة التخرج
    public decimal TotalGrade { get; set; } // مجموع الدرجات
    public AdmissionType AdmissionType { get; set; } = AdmissionType.None;// نوع القبول
}

using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Enums;

namespace Universe.Core.Contracts.User;

public record PreviousQualificationResponse(
     string SchoolName,
     string EnrollmentYear, // سنة الالتحاق
     int SeatNumber, // رقم الجلوس
     string Qualification, // المؤهل
     string GraduationYear, // سنة التخرج
     decimal TotalGrade, // مجموع الدرجات
     AdmissionType? AdmissionType // نوع القبول
);

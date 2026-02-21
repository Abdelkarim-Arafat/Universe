using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Enums;

namespace Universe.Application.UserServices.UserDtos;

public record PreviousQualificationResponse(
     string SchoolName,
     string EnrollmentYear, // سنة الالتحاق
     int SeatNumber, // رقم الجلوس
     string Qualification, // المؤهل
     string GraduationYear, // سنة التخرج
     decimal TotalGrade, // مجموع الدرجات
     AdmissionType AdmissionType // نوع القبول
);

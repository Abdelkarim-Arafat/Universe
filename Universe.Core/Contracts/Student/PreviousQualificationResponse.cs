using Universe.Core.Enums;

namespace Universe.Core.Contracts.Student;

public record PreviousQualificationResponse(
     string SchoolName,
     string EnrollmentYear, // سنة الالتحاق
     int SeatNumber, // رقم الجلوس
     string Qualification, // المؤهل
     string GraduationYear, // سنة التخرج
     decimal TotalGrade, // مجموع الدرجات
     AdmissionType? AdmissionType // نوع القبول
);

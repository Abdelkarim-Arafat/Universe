using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.User;
using Universe.Core.Enums;

namespace Universe.Application.UserServices.Commands.UpdatePreviousQualification;

public record UpdatePreviousQualificationCommand(
     [Required] Guid StudentId,
     string SchoolName,
     string EnrollmentYear, // سنة الالتحاق
     int SeatNumber, // رقم الجلوس
     string Qualification, // المؤهل
     string GraduationYear, // سنة التخرج
     decimal TotalGrade, // مجموع الدرجات
     AdmissionType AdmissionType // نوع القبول
) : IRequest<Result<PreviousQualificationResponse>>;

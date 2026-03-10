using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Universe.Core.Entities.Core;
using Universe.Core.Entities.StudentInfo;
using Universe.Core.Enums;

namespace Universe.Core.Entities;

public class Student : BaseEntity
{
    public Guid Id { get; set; }
    public ApplicationUser ApplicationUser { get; set; } = default!;
    public string Name { get; set; } = string.Empty;
    public string StudentCode { get; set; } = string.Empty; // كود الطالب
    public string ImageUrl { get; set; } = string.Empty;
    public Gender? Gender { get; set; } // النوع
    public Religion? Religion { get; set; }// الديانة
    public DateOnly? DateOfBirth { get; set; } // تاريخ الميلاد
    public string PlaceOfBirth { get; set; } = string.Empty; // محل الميلاد
    public string Nationality { get; set; } = string.Empty; // بلد الجنسية

    public string NationalIdOrPassport { get; set; } = string.Empty; // الرقم القومي او جواز السفر

    public MaritalStatus? MaritalStatus { get; set; } // الحالة الاجتماعية
    public ContactInfo ContactInfo { get; set; } = default!; // بيانات الاتصال
    public ParentInfo ParentInfo { get; set; } = default!; // بيانات ولي الامر
    public PreviousQualification PreviousQualification { get; set; } = default!; // بيانات المؤهل السابق
    public MilitaryInfo? MilitaryInfo { get; set; } // بيانات التجنيد
    public ICollection<StudentAcademicProgram> StudentAcademicPrograms { get; set; } = [];

    public Guid CollegeId { get; set; }
    public College College { get; set; } = default!;
}

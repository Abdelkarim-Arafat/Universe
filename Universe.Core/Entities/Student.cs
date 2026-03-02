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
    public Student() { Id = Guid.CreateVersion7(); }
    public Guid UserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; } = default!;
    public string ArabicName { get; set; } = string.Empty; 
    public string EnglishName { get; set; } = string.Empty; 
    public string StudentCode { get; set; } = string.Empty; // كود الطالب
    public string ImageUrl { get; set; } = string.Empty;
    public Gender Gender { get; set; } // النوع
    public Religion Religion { get; set; } = Religion.None;// الديانة

    public DateOnly? DateOfBirth { get; set; } // تاريخ الميلاد
    public string PlaceOfBirth { get; set; } = string.Empty; // محل الميلاد
    public string Nationality { get; set; } = string.Empty; // بلد الجنسية

    public string NationalIdOrPassport { get; set; } = string.Empty; // الرقم القومي او جواز السفر
    public MaritalStatus MaritalStatus { get; set; } = MaritalStatus.None;// الحالة الاجتماعية

    public ContactInfo ContactInfo { get; set; } = null!; // بيانات الاتصال
    public ParentInfo ParentInfo { get; set; } = null!; // بيانات ولي الامر
    public PreviousQualification PreviousQualification { get; set; } = null!; // بيانات المؤهل السابق
    public MilitaryInfo MilitaryInfo { get; set; } = null!; // بيانات التجنيد

    public ICollection<StudentAcademicProgram> studentAcademicPrograms { get; set; } = [];
    public Guid CollegeId { get; set; }
    public College College { get; set; } = null!;
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Core.Entities.StudentInfo;

[Owned]
public class ParentInfo
{
    public string GuardianName { get; set; } = string.Empty; // اسم ولي الامر
    public string RelationshipDegree { get; set; } = string.Empty; // درجة القرابة
    public string MotherName { get; set; } = string.Empty; // اسم الام
    public string Job { get; set; } = string.Empty; // الوظيفة
    public string GuardianCity { get; set; } = string.Empty; // المدينة
    public string GuardianAddress { get; set; } = string.Empty; // العنوان
    public string GuardianPhoneNumber { get; set; } = string.Empty; // التليفون
    public string GuardianEmail { get; set; } = string.Empty; // البريد الالكتروني
}

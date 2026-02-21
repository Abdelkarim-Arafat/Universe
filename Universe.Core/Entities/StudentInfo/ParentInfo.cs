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
    public string City { get; set; } = string.Empty; // المدينة
    public string Address { get; set; } = string.Empty; // العنوان
    public string Phone { get; set; } = string.Empty; // التليفون
    public string Email { get; set; } = string.Empty; // البريد الالكتروني
}

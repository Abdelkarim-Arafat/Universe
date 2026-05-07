using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Core.Constants;

public static class Roles
{
    public const string Admin = "Admin";
    public const string AcademicAdvising = "AcademicAdvising";
    public const string Staff = "Staff";
    public const string Student = "Student";

    public const string AdminOrAdvisor = $"{Admin},{AcademicAdvising}";
    public const string AdminOrAdvisorOrStaff = $"{Admin},{AcademicAdvising},{Staff}";
    public const string AllRoles = $"{Admin},{AcademicAdvising},{Staff},{Student}";
}
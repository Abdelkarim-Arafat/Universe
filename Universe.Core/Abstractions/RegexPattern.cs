using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Core.Abstractions;

public static class RegexPatterns
{
    public const string PhoneNumber = @"^(?:\+20|0020|20|0)?1[0125]\d{8}$";
    public const string Password = "(?=(.*[0-9]))(?=.*[\\!@#$%^&*()\\\\[\\]{}\\-_+=~`|:;\"'<>,./?])(?=.*[a-z])(?=(.*[A-Z]))(?=(.*)).{8,}";
}
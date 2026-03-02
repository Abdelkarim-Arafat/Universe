using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Infrastructure.SeedData;

// https://generateuuid.online/uuid/v7
public static class DefaultUsers
{
    // Admin
    public const string SVNU = "SVNU";
    public static readonly Guid SVNUId = Guid.Parse("019c0582-3473-7802-8f11-50cc1e6513d5");
    public const string SVNUEmail = "SVNU@Universe.com";
    public const string SVNUPassword = "AQAAAAIAAYagAAAAEFhcy5yaaQ5/9U5cfv8MnI3DBzUZ0ido47Hf7N0qKI20sJp8yGuUPuwPOIGdkNQJjA==";
    public const string SVNUSecurityStamp = "55BF92C9EF0249CDA210D85D1A851BC9";
    public const string SVNUConcurrencyStamp = "01993360-1c17-7054-bdee-1d5a9e780f23";

    // AcademicAdvising
    public const string AcademicAdvising = "Admin";
    public static readonly Guid AcademicAdvisingId = Guid.Parse("019c1e76-6f5a-7522-8327-a2a72adbbbe8");
    public const string AcademicAdvisingEmail = "Admin@Universe.com";
    public const string AcademicAdvisingPassword = "AQAAAAIAAYagAAAAEFhcy5yaaQ5/9U5cfv8MnI3DBzUZ0ido47Hf7N0qKI20sJp8yGuUPuwPOIGdkNQJjA==";
    public const string AcademicAdvisingSecurityStamp = "96A84AD3C17B4EBD95CE5AC8266BD761";
    public const string AcademicAdvisingConcurrencyStamp = "019c1e76-a7da-76a1-a6c8-96163fb4a2fc";
}
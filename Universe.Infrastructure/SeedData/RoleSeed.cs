using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Infrastructure.SeedData;

public static class RoleSeed
{
    public partial class Admin
    {
        public const string Name = nameof(Admin);
        public static readonly Guid Id = Guid.Parse("0191a4b6-c4fc-752e-9d95-40b5e4e68054");
        public const string ConcurrencyStamp = "0191a4b6-c4fc-752e-9d95-40b631d1866d";
        public const int Level = 1;
    }
    public partial class AcademicAdvising
    {
        public const string Name = nameof(AcademicAdvising);
        public static readonly Guid Id = Guid.Parse("019c1e6e-5518-7479-b749-b1c5d4a21430");
        public const string ConcurrencyStamp = "019c1e6e-8a41-7129-a8f1-28dc8a042458";
        public const int Level = 2;
    }
    public partial class Staff
    {
        public const string Name = nameof(Staff);
        public static readonly Guid Id = Guid.Parse("019c1e67-90d0-72a4-a602-9a98388515e9");
        public const string ConcurrencyStamp = "019c1e68-2418-723e-8a28-5638fd18e4e7";
        public const int Level = 3;
    }
    public partial class Student
    {
        public const string Name = nameof(Student);
        public static readonly Guid Id = Guid.Parse("0191a4b6-c4fc-752e-9d95-40b7a5cb88f0");
        public const string ConcurrencyStamp = "0191a4b6-c4fc-752e-9d95-40b85cf3fd22";
        public const int Level = 4;
    }
}
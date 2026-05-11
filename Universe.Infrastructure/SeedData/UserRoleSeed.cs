using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Infrastructure.SeedData;

public static class UserRoleSeed
{
    public static readonly IdentityUserRole<Guid>[] Data =
    {
        // Student
        new IdentityUserRole<Guid> { UserId = Guid.Parse("019e0b3a-fc8d-731a-a8b0-4bfaabcc96ca"), RoleId = RoleSeed.Student.Id },
        new IdentityUserRole<Guid> { UserId = Guid.Parse("019e0b3a-fc8d-731a-a8b0-4bfbed8bcad6"), RoleId = RoleSeed.Student.Id },
        new IdentityUserRole<Guid> { UserId = Guid.Parse("019e0b3a-fc8d-731a-a8b0-4bfc23663301"), RoleId = RoleSeed.Student.Id },
        new IdentityUserRole<Guid> { UserId = Guid.Parse("019e0b3a-fc8d-731a-a8b0-4bfdb98afc89"), RoleId = RoleSeed.Student.Id },
        new IdentityUserRole<Guid> { UserId = Guid.Parse("019e0b3a-fc8d-731a-a8b0-4bfe50d17ab6"), RoleId = RoleSeed.Student.Id },
        new IdentityUserRole<Guid> { UserId = Guid.Parse("019e0b3a-fc8d-731a-a8b0-4bffcca69ba2"), RoleId = RoleSeed.Student.Id },
        new IdentityUserRole<Guid> { UserId = Guid.Parse("019e0b3a-fc8d-731a-a8b0-4c00fde85117"), RoleId = RoleSeed.Student.Id },
        new IdentityUserRole<Guid> { UserId = Guid.Parse("019e0b3a-fc8d-731a-a8b0-4c01dc56ab84"), RoleId = RoleSeed.Student.Id },
        new IdentityUserRole<Guid> { UserId = Guid.Parse("019e0b3a-fc8d-731a-a8b0-4c023955bc55"), RoleId = RoleSeed.Student.Id },
        new IdentityUserRole<Guid> { UserId = Guid.Parse("019e0b3a-fc8d-731a-a8b0-4c03d6d4a539"), RoleId = RoleSeed.Student.Id },
        new IdentityUserRole<Guid> { UserId = Guid.Parse("019e0b3a-fc8d-731a-a8b0-4c049c362ec7"), RoleId = RoleSeed.Student.Id },
        new IdentityUserRole<Guid> { UserId = Guid.Parse("019e0b3a-fc8d-731a-a8b0-4c05819c53dd"), RoleId = RoleSeed.Student.Id },
        new IdentityUserRole<Guid> { UserId = Guid.Parse("019e0b3a-fc8d-731a-a8b0-4c0690d58a99"), RoleId = RoleSeed.Student.Id },
        new IdentityUserRole<Guid> { UserId = Guid.Parse("019e0b3a-fc8d-731a-a8b0-4c078a922c15"), RoleId = RoleSeed.Student.Id },
        new IdentityUserRole<Guid> { UserId = Guid.Parse("019e0b3a-fc8d-731a-a8b0-4c081a94e1da"), RoleId = RoleSeed.Student.Id },
        new IdentityUserRole<Guid> { UserId = Guid.Parse("019e0b3a-fc8d-731a-a8b0-4c09fa3a41a4"), RoleId = RoleSeed.Student.Id },
        new IdentityUserRole<Guid> { UserId = Guid.Parse("019e0b3a-fc8d-731a-a8b0-4c0a65d5f6f9"), RoleId = RoleSeed.Student.Id },
        new IdentityUserRole<Guid> { UserId = Guid.Parse("019e0b3a-fc8d-731a-a8b0-4c0b2f3805de"), RoleId = RoleSeed.Student.Id },
        new IdentityUserRole<Guid> { UserId = Guid.Parse("019e0b3a-fc8d-731a-a8b0-4c0c076cbbd5"), RoleId = RoleSeed.Student.Id },
        new IdentityUserRole<Guid> { UserId = Guid.Parse("019e0b3a-fc8d-731a-a8b0-4c0d122b4ffd"), RoleId = RoleSeed.Student.Id },


        // Staff
        new IdentityUserRole<Guid> { UserId = Guid.Parse("019e0c87-1cbb-74c7-884a-9916db0669c4"), RoleId = RoleSeed.Staff.Id },
        new IdentityUserRole<Guid> { UserId = Guid.Parse("019e0c87-1cbb-74c7-884a-9916db0669c4"), RoleId = RoleSeed.AcademicAdvising.Id },

        new IdentityUserRole<Guid> { UserId = Guid.Parse("019e0c87-1cbb-74c7-884a-991784974e4e"), RoleId = RoleSeed.Staff.Id },
        new IdentityUserRole<Guid> { UserId = Guid.Parse("019e0c87-1cbb-74c7-884a-991784974e4e"), RoleId = RoleSeed.AcademicAdvising.Id },

        new IdentityUserRole<Guid> { UserId = Guid.Parse("019e0c87-1cbb-74c7-884a-99181912a6db"), RoleId = RoleSeed.Staff.Id },
        new IdentityUserRole<Guid> { UserId = Guid.Parse("019e0c87-1cbb-74c7-884a-99195dbd3256"), RoleId = RoleSeed.Staff.Id },
        new IdentityUserRole<Guid> { UserId = Guid.Parse("019e0c87-1cbb-74c7-884a-991ac3846834"), RoleId = RoleSeed.Staff.Id },
        new IdentityUserRole<Guid> { UserId = Guid.Parse("019e0c87-1cbb-74c7-884a-991bed8c312c"), RoleId = RoleSeed.Staff.Id },
        new IdentityUserRole<Guid> { UserId = Guid.Parse("019e0c87-1cbb-74c7-884a-991ccb84275b"), RoleId = RoleSeed.Staff.Id },
        new IdentityUserRole<Guid> { UserId = Guid.Parse("019e0c87-1cbc-712f-90d1-61c9cbe6e293"), RoleId = RoleSeed.Staff.Id },
        new IdentityUserRole<Guid> { UserId = Guid.Parse("019e0c87-1cbc-712f-90d1-61ca6cf7a3b7"), RoleId = RoleSeed.Staff.Id },
        new IdentityUserRole<Guid> { UserId = Guid.Parse("019e0c87-1cbc-712f-90d1-61cb963bd6a2"), RoleId = RoleSeed.Staff.Id },
    };
}

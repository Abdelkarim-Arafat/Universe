using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities;

namespace Universe.Infrastructure.SeedData;

public static class ApplicationUserSeed
{
    public static readonly ApplicationUser[] Data =
    {
        
        // Staff
        new ApplicationUser { Id = Guid.Parse("019e0c87-1cbb-74c7-884a-9916db0669c4"), PasswordHash = "AQAAAAIAAYagAAAAEFhcy5yaaQ5/9U5cfv8MnI3DBzUZ0ido47Hf7N0qKI20sJp8yGuUPuwPOIGdkNQJjA==", Name = "Ahmed Adel", UserName = "staff1", NormalizedUserName = "STAFF1", Email = "staff1@universe.edu", NormalizedEmail = "STAFF1@UNIVERSE.EDU", EmailConfirmed = true, CollegeId = CollegeSeed.Id, SecurityStamp = "a1", ConcurrencyStamp = "a1" },
        new ApplicationUser { Id = Guid.Parse("019e0c87-1cbb-74c7-884a-991784974e4e"), PasswordHash = "AQAAAAIAAYagAAAAEFhcy5yaaQ5/9U5cfv8MnI3DBzUZ0ido47Hf7N0qKI20sJp8yGuUPuwPOIGdkNQJjA==", Name = "Omar Samy", UserName = "staff2", NormalizedUserName = "STAFF2", Email = "staff2@universe.edu", NormalizedEmail = "STAFF2@UNIVERSE.EDU", EmailConfirmed = true, CollegeId = CollegeSeed.Id, SecurityStamp = "a2", ConcurrencyStamp = "a2" },
        new ApplicationUser { Id = Guid.Parse("019e0c87-1cbb-74c7-884a-99181912a6db"), PasswordHash = "AQAAAAIAAYagAAAAEFhcy5yaaQ5/9U5cfv8MnI3DBzUZ0ido47Hf7N0qKI20sJp8yGuUPuwPOIGdkNQJjA==", Name = "Youssef Hassan", UserName = "staff3", NormalizedUserName = "STAFF3", Email = "staff3@universe.edu", NormalizedEmail = "STAFF3@UNIVERSE.EDU", EmailConfirmed = true, CollegeId = CollegeSeed.Id, SecurityStamp = "a3", ConcurrencyStamp = "a3" },
        new ApplicationUser { Id = Guid.Parse("019e0c87-1cbb-74c7-884a-99195dbd3256"), PasswordHash = "AQAAAAIAAYagAAAAEFhcy5yaaQ5/9U5cfv8MnI3DBzUZ0ido47Hf7N0qKI20sJp8yGuUPuwPOIGdkNQJjA==", Name = "Karim Mostafa", UserName = "staff4", NormalizedUserName = "STAFF4", Email = "staff4@universe.edu", NormalizedEmail = "STAFF4@UNIVERSE.EDU", EmailConfirmed = true, CollegeId = CollegeSeed.Id, SecurityStamp = "a4", ConcurrencyStamp = "a4" },
        new ApplicationUser { Id = Guid.Parse("019e0c87-1cbb-74c7-884a-991ac3846834"), PasswordHash = "AQAAAAIAAYagAAAAEFhcy5yaaQ5/9U5cfv8MnI3DBzUZ0ido47Hf7N0qKI20sJp8yGuUPuwPOIGdkNQJjA==", Name = "Ali Tarek", UserName = "staff5", NormalizedUserName = "STAFF5", Email = "staff5@universe.edu", NormalizedEmail = "STAFF5@UNIVERSE.EDU", EmailConfirmed = true, CollegeId = CollegeSeed.Id, SecurityStamp = "a5", ConcurrencyStamp = "a5" },
        new ApplicationUser { Id = Guid.Parse("019e0c87-1cbb-74c7-884a-991bed8c312c"), PasswordHash = "AQAAAAIAAYagAAAAEFhcy5yaaQ5/9U5cfv8MnI3DBzUZ0ido47Hf7N0qKI20sJp8yGuUPuwPOIGdkNQJjA==", Name = "Hassan Fathy", UserName = "staff6", NormalizedUserName = "STAFF6", Email = "staff6@universe.edu", NormalizedEmail = "STAFF6@UNIVERSE.EDU", EmailConfirmed = true, CollegeId = CollegeSeed.Id, SecurityStamp = "a6", ConcurrencyStamp = "a6" },
        new ApplicationUser { Id = Guid.Parse("019e0c87-1cbb-74c7-884a-991ccb84275b"), PasswordHash = "AQAAAAIAAYagAAAAEFhcy5yaaQ5/9U5cfv8MnI3DBzUZ0ido47Hf7N0qKI20sJp8yGuUPuwPOIGdkNQJjA==", Name = "Mahmoud Emad", UserName = "staff7", NormalizedUserName = "STAFF7", Email = "staff7@universe.edu", NormalizedEmail = "STAFF7@UNIVERSE.EDU", EmailConfirmed = true, CollegeId = CollegeSeed.Id, SecurityStamp = "a7", ConcurrencyStamp = "a7" },
        new ApplicationUser { Id = Guid.Parse("019e0c87-1cbc-712f-90d1-61c9cbe6e293"), PasswordHash = "AQAAAAIAAYagAAAAEFhcy5yaaQ5/9U5cfv8MnI3DBzUZ0ido47Hf7N0qKI20sJp8yGuUPuwPOIGdkNQJjA==", Name = "Mostafa Nabil", UserName = "staff8", NormalizedUserName = "STAFF8", Email = "staff8@universe.edu", NormalizedEmail = "STAFF8@UNIVERSE.EDU", EmailConfirmed = true, CollegeId = CollegeSeed.Id, SecurityStamp = "a8", ConcurrencyStamp = "a8" },
        new ApplicationUser { Id = Guid.Parse("019e0c87-1cbc-712f-90d1-61ca6cf7a3b7"), PasswordHash = "AQAAAAIAAYagAAAAEFhcy5yaaQ5/9U5cfv8MnI3DBzUZ0ido47Hf7N0qKI20sJp8yGuUPuwPOIGdkNQJjA==", Name = "Khaled Wael", UserName = "staff9", NormalizedUserName = "STAFF9", Email = "staff9@universe.edu", NormalizedEmail = "STAFF9@UNIVERSE.EDU", EmailConfirmed = true, CollegeId = CollegeSeed.Id, SecurityStamp = "a9", ConcurrencyStamp = "a9" },
        new ApplicationUser { Id = Guid.Parse("019e0c87-1cbc-712f-90d1-61cb963bd6a2"), PasswordHash = "AQAAAAIAAYagAAAAEFhcy5yaaQ5/9U5cfv8MnI3DBzUZ0ido47Hf7N0qKI20sJp8yGuUPuwPOIGdkNQJjA==", Name = "Ibrahim Hossam", UserName = "staff10", NormalizedUserName = "STAFF10", Email = "staff10@universe.edu", NormalizedEmail = "STAFF10@UNIVERSE.EDU", EmailConfirmed = true, CollegeId = CollegeSeed.Id, SecurityStamp = "a10", ConcurrencyStamp = "a10" },
    };
}

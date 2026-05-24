using Microsoft.AspNetCore.Identity;

namespace Universe.Core.Entities;

public class ApplicationRole : IdentityRole<Guid>
{
    public int Level { get; set; }
    public bool IsDefault { get; set; }
    public bool IsDeleted { get; set; }
}
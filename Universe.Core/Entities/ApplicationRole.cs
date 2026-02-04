using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Core.Entities;

public class ApplicationRole : IdentityRole<Guid>
{
    public bool IsDefault { get; set; }
    public bool IsDeleted { get; set; }
}
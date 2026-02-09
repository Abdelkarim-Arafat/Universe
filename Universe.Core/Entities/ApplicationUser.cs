using Microsoft.AspNetCore.Identity;
using Universe.Core.Entities.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Core.Entities;

public sealed class ApplicationUser : IdentityUser<Guid> , ISoftDeleteable
{
    public ApplicationUser() { Id = Guid.CreateVersion7(); }
    public string Name { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }

    public Guid CollegeId { get; set; }
    public College College { get; set; } = default!;

    public void UndoDelete()
    {
        IsDeleted = false;
        DeletedAt = null;
    }

    public ICollection<RefreshToken> RefreshTokens { get; set; } = [];
    public ICollection<PasswordResetOtp> passwordResetOtps { get; set; } = [];
}

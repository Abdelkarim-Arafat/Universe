using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Universe.Core.Entities;

[Owned]
public class PasswordResetOtp
{
    public string CodeHash { get; set; } = string.Empty;
    public bool IsVerified { get; set; }
    public bool isUsed { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime ExpiresAt { get; set; }
    public int Attempts { get; set; }
    public bool IsExpired => Attempts >= 5 || DateTime.UtcNow >= ExpiresAt || isUsed;
}

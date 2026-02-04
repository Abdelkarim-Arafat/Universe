using System.ComponentModel.DataAnnotations;

namespace Universe.Core.Abstractions;

public class MailSettings
{
    public static readonly string SectionName = "MailSettings";
    [Required]
    public string Host { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
    [Required]
    public string DisplayName { get; set; } = string.Empty;
    [Required]
    public string Mail { get; set; } = string.Empty;
    [Required]
    public int Port { get; set; }
}
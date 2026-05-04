using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Core.Abstractions.Options;

public class CloudinarySettings
{
    public static readonly string SectionName = "Cloudinary";
    public string CloudName { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
    public string ApiSecret { get; set; } = string.Empty;
    
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Core.Abstractions.Options;

public class PayPalSettings
{
    public static readonly string SectionName = "PayPal";
    public string ClientId { get; set; } = string.Empty;
    public string Secret { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = string.Empty;
}

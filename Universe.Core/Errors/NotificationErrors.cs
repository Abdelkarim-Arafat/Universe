using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Abstractions;

namespace Universe.Core.Errors;

public static class NotificationErrors
{
    public static Error NotFound => new(
        "Notification.NotFound",
        "The specified notification was not found.",
        StatusCodes.Status404NotFound
    );
}

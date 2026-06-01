using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Abstractions;

namespace Universe.Core.Errors;

public class MessageErrors
{
        public static readonly Error NotFound = new Error(
            "Message.NotFound",
            "Message not found.",
            StatusCodes.Status400BadRequest
        );

        public static readonly Error UnAuthorized = new Error(
            "Message.UnAuthorized",
            "You are not authorized to perform this action.",
            StatusCodes.Status403Forbidden
        );
}

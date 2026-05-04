using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Abstractions;

namespace Universe.Core.Errors;

public class FileErrors
{
    public static readonly Error EmptyFile = new Error(
        "File.Empty",
        "File is empty",
        StatusCodes.Status400BadRequest);

    public static readonly Error ImageUrlMismatch = new Error(
        "ImageUrl.Mismatch",
        "The provided old image URL does not match the user's current image URL.",
        StatusCodes.Status400BadRequest);
}

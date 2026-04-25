using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Abstractions;

namespace Universe.Core.Errors;

public class ServiceErrors
{
    public static readonly Error NotFound = new(
       "AcademicService.NotFound",
       "AcademicService is not found",
       StatusCodes.Status404NotFound);

    public static readonly Error RequestNotFound = new(
       "ServiceRequest.NotFound",
       "Request is not found",
       StatusCodes.Status404NotFound);
}

using Microsoft.AspNetCore.Mvc;
using Universe.Core.Abstractions;

namespace Universe.Api.Extensions;




public static class ResultExtensions
{
    //public static ObjectResult ToProblem(this Result result)
    //{
    //    if (result.IsSuccess)
    //        throw new InvalidOperationException();

    //    var problemDetails = new ProblemDetails
    //    {
    //        Status = result.Error.StatusCode,
    //        Title = result.Error.Code,
    //        Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1"
    //    };

    //    problemDetails.Extensions["errors"] = result.Error.Failures;

    //    return new ObjectResult(problemDetails)
    //    {
    //        StatusCode = result.Error.StatusCode
    //    };
    //}
    public static ObjectResult ToProblem(this Result result)
    {
        if (result.IsSuccess)
            throw new InvalidOperationException("can not convert success result to problem");

        var problem = Results.Problem(statusCode: result.Error.StatusCode , title: result.Error.Code);


        var problemDetails = problem.GetType()
            .GetProperty(nameof(ProblemDetails))!.GetValue(problem) as ProblemDetails;

        if (result.Error.Failures is not null)
            problemDetails!.Extensions["errors"] = result.Error.Failures;
        else problemDetails!.Extensions["errors"] = new[] { result.Error.Description };


        return new ObjectResult(problemDetails);
    }
}


//public static class ResultExtensions
//{
//    public static ObjectResult ToProblem(this Result result)
//    {
//        if (result.IsSuccess)
//            throw new InvalidOperationException("can not convert success result to problem");

//        var problem = Results.Problem(statusCode: result.Error.StatusCode);


//        var problemDetails = problem.GetType()
//            .GetProperty(nameof(ProblemDetails))!.GetValue(problem) as ProblemDetails;


//        problemDetails!.Extensions = new Dictionary<string, Object?>
//        {
//            { "error" , new[] {
//                result.Error
//            }
//            }
//        };

//        //var problemDetails = new ProblemDetails
//        //{
//        //    Type =
//        //    Status = statusCode,
//        //    Title = title,
//        //    Extensions = new Dictionary<string, Object?>
//        //    {
//        //        { "error" , new[] { result.Error } }
//        //    }
//        //};
//        return new ObjectResult(problemDetails);
//    }
//}
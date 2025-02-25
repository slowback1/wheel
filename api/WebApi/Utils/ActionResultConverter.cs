using Common.Data;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Utils;

public static class ActionResultConverter
{
    public static ActionResult ToActionResult<T>(this FeatureResult<T> featureResult)
    {
        return featureResult.Status switch
        {
            FeatureResultStatus.Ok => new OkObjectResult(featureResult.Data),
            FeatureResultStatus.NotFound => new NotFoundObjectResult(new ErrorResponse(404, "Not Found")),
            FeatureResultStatus.Error => new ObjectResult(new ErrorResponse(500, featureResult.Exception?.Message))
                { StatusCode = 500 },
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}

public class ErrorResponse
{
    public ErrorResponse()
    {
    }

    public ErrorResponse(int code, string message)
    {
        Code = code;
        Message = message;
    }

    public int Code { get; set; }
    public string Message { get; set; }
}
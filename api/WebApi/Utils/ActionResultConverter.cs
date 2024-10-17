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
            FeatureResultStatus.NotFound => new NotFoundResult(),
            FeatureResultStatus.Error => new ObjectResult(featureResult.Exception?.Message) { StatusCode = 500 },
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}
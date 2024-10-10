using Common.Data;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public abstract class ControllerBase : Controller
{
    protected ActionResult ToActionResult<T>(FeatureResult<T> featureResult)
    {
        return featureResult.Status switch
        {
            FeatureResultStatus.Ok => Ok(featureResult.Data),
            FeatureResultStatus.NotFound => NotFound(),
            FeatureResultStatus.Error => Problem(featureResult.Exception?.Message),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}
using Common.Data;
using Common.Interfaces;
using Infrastructure.Messaging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApi.Config;
using WebApi.Utils;

namespace WebApi.Controllers;

public abstract class ControllerBase : Controller
{
    protected readonly IDataAccess _dataAccess;

    protected ControllerBase()
    {
        var options = MessageBus.GetLastMessage<StorageConfig>(Messages.StorageOptions);

        _dataAccess = DataAccessFactory.CreateDataAccess(options);
    }

    protected string UserToken { get; set; }

    protected ActionResult ToActionResult<T>(FeatureResult<T> featureResult)
    {
        return featureResult.ToActionResult();
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);

        var headers = context.HttpContext.Request.Headers;

        if (headers.ContainsKey("X-User-Token")) UserToken = headers["X-User-Token"].ToString();
    }
}
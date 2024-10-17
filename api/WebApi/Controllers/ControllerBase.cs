using Common.Data;
using Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApi.Utils;

namespace WebApi.Controllers;

public abstract class ControllerBase : Controller
{
    protected readonly IDataAccess _dataAccess;

    protected ControllerBase()
    {
        _dataAccess = DataAccessFactory.CreateDataAccess();
    }

    protected ActionResult ToActionResult<T>(FeatureResult<T> featureResult)
    {
        return featureResult.ToActionResult();
    }
}
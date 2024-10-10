using Common.Interfaces;
using Data;
using Features;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("Wheel")]
public class WheelController : ControllerBase
{
    private readonly IWheelRetriever _wheelRetriever;

    public WheelController()
    {
        _wheelRetriever = new WheelRetriever();
    }

    [Route("{wheelId}")]
    [HttpGet]
    public ActionResult GetWheel(string wheelId)
    {
        var result = new WheelFeatures(_wheelRetriever).GetWheelSetting(wheelId).Result;
        return ToActionResult(result);
    }

    [Route("")]
    [HttpGet]
    public ActionResult GetWheels()
    {
        var result = new WheelFeatures(_wheelRetriever).GetWheelSettings().Result;
        return ToActionResult(result);
    }
}
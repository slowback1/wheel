using Common.Data;
using Features;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("Wheel")]
public class WheelController : ControllerBase
{
    private readonly WheelFeatures _wheelFeatures;

    public WheelController()
    {
        _wheelFeatures = new WheelFeatures(_dataAccess.WheelRetriever, _dataAccess.WheelCreator);
    }


    [Route("{wheelId}")]
    [HttpGet]
    public async Task<ActionResult> GetWheel(string wheelId)
    {
        var result = await _wheelFeatures.GetWheelSetting(wheelId);
        return ToActionResult(result);
    }

    [Route("")]
    [HttpGet]
    public async Task<ActionResult> GetWheels()
    {
        var result = await _wheelFeatures.GetWheelSettings();
        return ToActionResult(result);
    }

    [Route("")]
    [HttpPost]
    public async Task<ActionResult> CreateWheel([FromBody] WheelSetting wheelSetting)
    {
        var result = await _wheelFeatures.CreateWheelSetting(wheelSetting);
        return ToActionResult(result);
    }
}
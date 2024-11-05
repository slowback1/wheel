using Common.Data;
using Microsoft.AspNetCore.Mvc;
using UseCases.Wheel;

namespace WebApi.Controllers;

[ApiController]
[Route("Wheel")]
public class WheelController : ControllerBase
{
    [Route("{wheelId}")]
    [HttpGet]
    public async Task<ActionResult> GetWheel(string wheelId)
    {
        var useCase = new GetWheelSettingUseCase(_dataAccess);

        var result = await useCase.GetWheelSetting(wheelId);
        return ToActionResult(result);
    }

    [Route("")]
    [HttpGet]
    public async Task<ActionResult> GetWheels()
    {
        var useCase = new GetWheelSettingsUseCase(_dataAccess);

        var result = await useCase.GetWheelSettings(UserToken);
        return ToActionResult(result);
    }

    [Route("")]
    [HttpPost]
    public async Task<ActionResult> CreateWheel([FromBody] CreateWheelSetting wheelSetting)
    {
        var useCase = new CreateWheelSettingUseCase(_dataAccess);

        var result = await useCase.CreateWheelSetting(wheelSetting, UserToken);
        return ToActionResult(result);
    }
}
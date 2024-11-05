using Microsoft.AspNetCore.Mvc;
using UseCases.Spinning;
using WebApi.RequestModels;

namespace WebApi.Controllers;

[Route("Spin")]
public class SpinController : ControllerBase
{
    [Route("Spin")]
    [HttpPost]
    public ActionResult Spin([FromBody] WheelSpinRequest request)
    {
        var useCase = new WheelSpinningUseCase();

        var result = useCase.SpinTheWheel(request.WheelSetting, request.Options);
        return ToActionResult(result);
    }
}
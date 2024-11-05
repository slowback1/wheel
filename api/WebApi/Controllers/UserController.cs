using Common.Data;
using Microsoft.AspNetCore.Mvc;
using UseCases.User;

namespace WebApi.Controllers;

[Route("User")]
public class UserController : ControllerBase
{
    [Route("Register")]
    [HttpPost]
    public async Task<ActionResult> Register([FromBody] CreateUser user)
    {
        var useCase = new RegisterUserUseCase(_dataAccess);

        var result = await useCase.Register(user);
        return ToActionResult(result);
    }

    [Route("Login")]
    [HttpPost]
    public async Task<ActionResult> Login([FromBody] LoginUser user)
    {
        var useCase = new LoginUseCase(_dataAccess);

        var result = await useCase.Login(user.Username, user.Password);
        return ToActionResult(result);
    }
}
using IQT.Authorization.API.DTO;
using IQT.Authorization.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace IQT.Authorization.API.Controllers;

[Produces("application/json")]
[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{

    public AuthController()
    {

    }

    [HttpPost]
    public async Task<IActionResult> SignInAsync([FromBody] AccessCredentialsDTO credentials, [FromServices] AuthService _authService)
    {
        var response = await _authService.GenerateTokenAsync(credentials.Email);
        return Ok(response);
    }

}


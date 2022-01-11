using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IQT.Authorization.API.Controllers;

[Produces("application/json")]
[Route("api/test")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class TestsController : ControllerBase
{

    [HttpGet("admin")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> TestAdmin()
    {
        return Ok();
    }

    [HttpGet("manager")]
    [Authorize(Roles = "Manager")]
    public async Task<IActionResult> TestManager()
    {
        return Ok();
    }

    [HttpGet("policy-admin")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<IActionResult> TestPolicyAdmin()
    {
        return Ok();
    }

    [HttpGet("policy-manager")]
    [Authorize(Policy = "ManagerPolicy")]
    public async Task<IActionResult> TestPolicyManager()
    {
        return Ok();
    }

    [HttpGet("policy-gender")]
    [Authorize(Policy = "GenderPolicy")]
    public async Task<IActionResult> TestPolicyGender()
    {
        return Ok();
    }

    [HttpGet("policy-department")]
    [Authorize(Policy = "DepartmentPolicy")]
    public async Task<IActionResult> TestPolicyDepartment()
    {
        return Ok();
    }


    [HttpGet("policy-minimum-age")]
    [Authorize(Policy = "MinimumAgePolicy")]
    public async Task<IActionResult> TestPolicyMinimumAge()
    {
        return Ok();
    }

}


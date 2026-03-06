using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MovieApi.Api.Controllers;

[ApiController]
[Route("api/test")]
public sealed class TestController : ControllerBase
{
    [HttpGet("public")]
    public IActionResult Public()
    {
        return Ok(new { message = "Public endpoint OK" });
    }

    [Authorize]
    [HttpGet("private")]
    public IActionResult Private()
    {
        return Ok(new { message = "Private endpoint OK" });
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("admin")]
    public IActionResult Admin()
    {
        return Ok(new { message = "Admin endpoint OK" });
    }
}
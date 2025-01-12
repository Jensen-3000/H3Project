using H3Project.Data.DTOs.Users;
using H3Project.Data.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace H3Project.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> GenerateToken([FromBody] UserLoginDto loginDto)
    {
        var token = await _authService.GenerateTokenAsync(loginDto);

        if (token == null)
        {
            return Unauthorized();
        }

        return Ok(new { token });
    }
}

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using H3Project.Data.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace H3Project.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly JwtSettings _jwtSettings;
    public AuthController(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }

    [HttpPost("token")]
    public IActionResult GenerateToken()
    {
        var claims = new[]
        {
           new Claim(JwtRegisteredClaimNames.Sub  , "TestUser"),
           new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
           new Claim("Onsdag", "Palle"),
           new Claim(ClaimTypes.Role, "Sammy")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(_jwtSettings.ExpireMinutes),
            signingCredentials: creds
        );

        return Ok(new JwtSecurityTokenHandler().WriteToken(token));
    }
}
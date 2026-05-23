using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RevRecrutAPI.Entities.User;
using RevRecrutAPI.DTOs.UserDto;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using RevRecrutAPI.Services.Auth;


namespace RevRecrutAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
    public static User user = new();

    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(UserDto request)
    {
        var user = await authService.RegisterAsync(request);
        if (user is null)
        {
            return BadRequest("Username already exists");
        }
        return Ok(user);
    }

    [HttpPost("login")]
    public async Task<ActionResult<TokenResponse>> Login(UserDto request)
    {
        var result = await authService.LoginAsync(request);
        if (result is null)
        {
            return BadRequest("Invalid username or password");
        }

        return Ok(result);
    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult<TokenResponse>> RefreshToken(TokenRequest request)
    {
        var result = await authService.RefreshTokensAsync(request);
        if (result is null || result.AccessToken is null || result.Refresh is null)
        {
            return Unauthorized("Invalid refresh token");
        }

        return Ok(result);
    }
}

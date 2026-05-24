using Microsoft.AspNetCore.Mvc;
using RevRecrutAPI.Entities.User;
using RevRecrutAPI.DTOs.UserDto;
using RevRecrutAPI.Services.Auth;
using RevRecrutAPI.DTOs.UserLoginDto;
using RevRecrutAPI.DTOs.UserRegisterDto;


namespace RevRecrutAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
    public static User user = new();

    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(UserRegisterDto request)
    {
        var user = await authService.RegisterAsync(request);
        if (user is null)
        {
            return BadRequest("User already exists");
        }
        return Ok(user);
    }

    [HttpGet("confirm-email")]
    public async Task<ActionResult> ConfirmEmail([FromQuery] Guid userId, [FromQuery] string token)
    {
        var result = await authService.ConfirmEmailAsync(userId, token);
        if (!result)
            return BadRequest("Invalid token or user");

        return Ok("Email confirmed");
    }

    [HttpPost("login")]
    public async Task<ActionResult<TokenResponse>> Login(UserLoginDto request)
    {
        var result = await authService.LoginAsync(request);
        if (result is null)
        {
            return BadRequest("Invalid email or password or email not confirmed");
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

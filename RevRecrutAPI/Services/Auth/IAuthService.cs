using RevRecrutAPI.DTOs.UserDto;
using RevRecrutAPI.DTOs.UserLoginDto;
using RevRecrutAPI.DTOs.UserRegisterDto;
using RevRecrutAPI.Entities.User;

namespace RevRecrutAPI.Services.Auth;

public interface IAuthService
{
    Task<User?> RegisterAsync(UserRegisterDto request);
    Task<TokenResponse?> LoginAsync(UserLoginDto request);
    Task<TokenResponse?> RefreshTokensAsync(TokenRequest request);
    Task SendEmailConfirmationAsync(User user);
    Task<bool> ConfirmEmailAsync(Guid userId, string token);
}

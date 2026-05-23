using RevRecrutAPI.DTOs.UserDto;
using RevRecrutAPI.Entities.User;

namespace RevRecrutAPI.Services.Auth;

public interface IAuthService
{
    Task<User?> RegisterAsync(UserDto request);
    Task<TokenResponse?> LoginAsync(UserDto request);
    Task<TokenResponse?> RefreshTokensAsync(TokenRequest request);
}

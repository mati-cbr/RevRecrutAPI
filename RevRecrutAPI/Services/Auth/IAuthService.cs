using RevRecrutAPI.DTOs.UserDto;
using RevRecrutAPI.Entities.User;

namespace RevRecrutAPI.Services.Auth;

public interface IAuthService
{
    Task<User?> RegisterAsync(UserDto request);
    Task<string?> LoginAsync(UserDto request);
}

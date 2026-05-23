namespace RevRecrutAPI.DTOs.UserDto;

public class TokenRequest
{
    public Guid UserId { get; set; }
    public required string RefreshToken { get; set; }
}

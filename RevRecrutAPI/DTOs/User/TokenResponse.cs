namespace RevRecrutAPI.DTOs.UserDto;

public class TokenResponse
{
    public required string AccessToken { get; set; }
    public required string Refresh { get; set; }
}

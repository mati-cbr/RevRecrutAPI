using System.ComponentModel.DataAnnotations;

namespace RevRecrutAPI.DTOs.UserLoginDto;

public class UserLoginDto
{
    [Required(ErrorMessage = "E-mail is required")]
    public string? Email { get; set; }
    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace RevRecrutAPI.DTOs.UserRegisterDto;

public class UserRegisterDto
{
    public string? FirstName { get; set; } = String.Empty;
    public string? LastName { get; set; } = String.Empty;
    [Required(ErrorMessage = "Email is required")]
    public string? Email { get; set; }
    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match")]
    public string? PasswordConfirm { get; set; }

}

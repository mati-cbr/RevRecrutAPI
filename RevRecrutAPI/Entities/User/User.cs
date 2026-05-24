using RevRecrutAPI.Entities.Candidate;
using System.ComponentModel.DataAnnotations;

namespace RevRecrutAPI.Entities.User;
public class User
{
    [Key]
    public Guid Id { get; set; }
    public string PasswordHash { get; set; } = String.Empty;
    public string? Email {  get; set; } = String.Empty;
    public string? Role { get; set; } = "Candidate";
    public Profile? profile { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefrreshTokenExpiryTime { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool EmailConfirmed { get; set; } = false;
    public string? EmailConfirmationToken { get; set; }
    public DateTime? EmailConfirmationTokenExpiry { get; set; }
}

using RevRecrutAPI.Entities.Candidate;
using System.ComponentModel.DataAnnotations;

namespace RevRecrutAPI.Entities.User;
public class User
{
    [Key]
    public Guid Id { get; set; }
    public string Username { get; set; } = String.Empty;
    public string PasswordHash { get; set; } = String.Empty;
    public string? Email {  get; set; } = String.Empty;
    public string? Role { get; set; } = "Candidate";
    public Profile? profile { get; set; }
}

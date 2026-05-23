using RevRecrutAPI.Entities.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RevRecrutAPI.Entities.Candidate;

public class Profile
{
    [Key]
    public Guid Id { get; set; }
    public Guid? UserId { get; set; }
    public User.User? User { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ReadableId { get; set; } = default;
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string ContactEMail { get; set; }
    public required string ContactPhone { get; set; }
    public string Address1 { get; set; } = string.Empty;
    public string Address2 { get; set; } = string.Empty;
}

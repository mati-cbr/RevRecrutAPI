namespace RevRecrutAPI.DTOs.Candidate.Profile
{
    public class ProfileResponse
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string ContactEMail { get; set; }
        public required string ContactPhone { get; set; }
        public string Address1 { get; set; } = string.Empty;
        public string Address2 { get; set; } = string.Empty;
    }
}

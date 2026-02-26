namespace RevRecrutAPI.Services.Candidate.Profile;

public interface IProfileService
{
    Task<List<Models.Candidate.Profile>> GetAllProfilesAsync();
    Task<Models.Candidate.Profile?> GetProfileByIdAsync(int id);
    Task<Models.Candidate.Profile> AddProfileAsync(Models.Candidate.Profile profile);
    Task<bool> UpdateProfileAsync(int id, Models.Candidate.Profile profile);
    Task<bool> DeleteProfileAsync(int id);
}

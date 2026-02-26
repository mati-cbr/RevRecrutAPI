namespace RevRecrutAPI.Services.Candidate.Profile;

public interface IProfileService
{
    Task<List<DTOs.Candidate.Profile.ProfileResponse>> GetAllProfilesAsync();
    Task<DTOs.Candidate.Profile.ProfileResponse?> GetProfileByIdAsync(int id);
    Task<DTOs.Candidate.Profile.ProfileResponse> AddProfileAsync(DTOs.Candidate.Profile.CreateProfileRequest profile);
    Task<bool> UpdateProfileAsync(int id, Entities.Candidate.Profile profile);
    Task<bool> DeleteProfileAsync(int id);
}

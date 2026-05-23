namespace RevRecrutAPI.Services.Candidate.Profile;

public interface IProfileService
{
    IQueryable<DTOs.Candidate.Profile.ProfileResponse> GetAllProfiles();
    IQueryable<DTOs.Candidate.Profile.ProfileResponse?> GetProfileById(int id);
    Task<DTOs.Candidate.Profile.ProfileResponse> AddProfileAsync(DTOs.Candidate.Profile.CreateProfileRequest profile);
    Task<bool> UpdateProfileAsync(int id, Entities.Candidate.Profile profile);
    Task<bool> DeleteProfileAsync(int id);
}


using Microsoft.EntityFrameworkCore;
using RevRecrutAPI.DB;
using RevRecrutAPI.DTOs.Candidate.Profile;
using System.Linq;

namespace RevRecrutAPI.Services.Candidate.Profile;

public class ProfileService(AppDbContext context) : IProfileService
{
    public async Task<ProfileResponse> AddProfileAsync(CreateProfileRequest profile)
    {
        var newProfile = new Entities.Candidate.Profile
        {
            FirstName = profile.FirstName,
            LastName = profile.LastName,
            ContactEMail = profile.ContactEMail,
            ContactPhone = profile.ContactPhone,
            Address1 = profile.Address1,
            Address2 = profile.Address2,
        };

        context.Profiles.Add(newProfile);
        await context.SaveChangesAsync();

        return new ProfileResponse
        {
            Id = newProfile.Id,
            FirstName = newProfile.FirstName,
            LastName = newProfile.LastName,
            ContactEMail = newProfile.ContactEMail,
            ContactPhone = newProfile.ContactPhone,
            Address1 = newProfile.Address1,
            Address2 = newProfile.Address2,
        };
    }

    public Task<bool> DeleteProfileAsync(int id)
    {
        throw new NotImplementedException();
    }

    public IQueryable<ProfileResponse> GetAllProfiles()
        => context.Profiles.Select(p => new ProfileResponse
        {
            Id = p.Id,
            FirstName = p.FirstName,
            LastName = p.LastName,
            ContactEMail = p.ContactEMail,
            ContactPhone = p.ContactPhone,
            Address1 = p.Address1,
            Address2 = p.Address2,
        });

    public IQueryable<ProfileResponse?> GetProfileById(int id)
    {
        IQueryable<ProfileResponse> result = context.Profiles
            .Where(p => p.Id == id)
            .Select(p => new ProfileResponse
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                ContactEMail = p.ContactEMail,
                ContactPhone = p.ContactPhone,
                Address1 = p.Address1,
                Address2 = p.Address2,
            });

        return result;
    }

    public Task<bool> UpdateProfileAsync(int id, Entities.Candidate.Profile profile)
    {
        throw new NotImplementedException();
    }
}

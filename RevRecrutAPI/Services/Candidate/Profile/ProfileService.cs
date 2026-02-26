
namespace RevRecrutAPI.Services.Candidate.Profile;

public class ProfileService : IProfileService
{
    private static readonly List<Models.Candidate.Profile> profiles =
    [
    new Models.Candidate.Profile { Id = 1, FirstName = "Jan", LastName = "Kowalski", ContactEMail = "jan@o2.pl", ContactPhone = "123321123" },
            new Models.Candidate.Profile { Id = 2, FirstName = "Kazimierz", LastName = "Heinz", ContactEMail = "kazik@wp.pl", ContactPhone = "333222111" },
            new Models.Candidate.Profile { Id = 3, FirstName = "Mariusz", LastName = "Alkoholik", ContactEMail = "pantadeuszpollitra@interia.pl", ContactPhone = "222222222" },
            new Models.Candidate.Profile { Id = 4, FirstName = "Mścichuj", LastName = "Biały", ContactEMail = "chuj@gmail.pl", ContactPhone = "999999999" }
    ];
    public Task<Models.Candidate.Profile> AddProfileAsync(Models.Candidate.Profile profile)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteProfileAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Models.Candidate.Profile>> GetAllProfilesAsync()
        => await Task.FromResult(profiles);

    public async Task<Models.Candidate.Profile?> GetProfileByIdAsync(int id)
    {
        var result = profiles.FirstOrDefault(p => p.Id == id);
        return await Task.FromResult(result);
    }

    public Task<bool> UpdateProfileAsync(int id, Models.Candidate.Profile profile)
    {
        throw new NotImplementedException();
    }
}

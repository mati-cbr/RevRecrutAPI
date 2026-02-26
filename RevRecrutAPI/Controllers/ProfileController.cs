using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using RevRecrutAPI.Models.Candidate;
using RevRecrutAPI.Services.Candidate.Profile;

namespace RevRecrutAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController(IProfileService service) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Profile>>> GetProfiles()
            => Ok(await service.GetAllProfilesAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult> GetProfile(int id)
        {
            var profile = await service.GetProfileByIdAsync(id);
            return profile is null ? NotFound("Profile with given ID was not found") : Ok(profile);
        }
    }
}

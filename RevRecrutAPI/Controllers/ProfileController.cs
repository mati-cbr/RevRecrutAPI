using Microsoft.AspNetCore.Mvc;
using RevRecrutAPI.DTOs.Candidate.Profile;
using RevRecrutAPI.Services.Candidate.Profile;

namespace RevRecrutAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProfileController(IProfileService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<ProfileResponse>>> GetProfiles()
        => Ok(await service.GetAllProfilesAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<ProfileResponse>> GetProfile(int id)
    {
        var profile = await service.GetProfileByIdAsync(id);
        return profile is null ? NotFound("Profile with given ID was not found") : Ok(profile);
    }

    [HttpPost]
    public async Task<ActionResult<ProfileResponse>> AddProfile(CreateProfileRequest profile)
    {
        var createdProfile = await service.AddProfileAsync(profile);
        return CreatedAtAction(nameof(GetProfile), new { id = createdProfile.Id }, createdProfile);
    }
}

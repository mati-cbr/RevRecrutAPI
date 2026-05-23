using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RevRecrutAPI.DTOs.Candidate.Profile;
using RevRecrutAPI.Services.Candidate.Profile;

namespace RevRecrutAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProfileController(IProfileService service) : ControllerBase
{
    [Authorize(Roles = "Admin, Candidate, Recruiter")]
    [HttpGet]
    public async Task<ActionResult<List<ProfileResponse>>> GetProfiles()
        => Ok(await service.GetAllProfiles().ToListAsync());

    [Authorize(Roles = "Admin, Candidate, Recruiter")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ProfileResponse>> GetProfile(Guid id)
    {
        var profile = service.GetProfileById(id);
        var result = await profile.FirstOrDefaultAsync();
        return result is null ? NotFound("Profile with given ID was not found") : Ok(profile);
    }

    [Authorize(Roles = "Admin, Candidate")]
    [HttpPost]
    public async Task<ActionResult<ProfileResponse>> AddProfile(CreateProfileRequest profile)
    {
        var createdProfile = await service.AddProfileAsync(profile);
        return CreatedAtAction(nameof(GetProfile), new { Id = createdProfile.Id }, createdProfile);
    }
}

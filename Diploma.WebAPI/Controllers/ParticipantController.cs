using Diploma.WebAPI.BusinessLogic.Interfaces;
using Diploma.WebAPI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.WebAPI.Controllers;

[ApiController]
[Route("api/tournaments/{tournamentId:guid}/participants")]
[Authorize]
public class ParticipantController : ControllerBase
{
    private readonly IParticipantService _participantService;

    public ParticipantController(IParticipantService participantService)
    {
        _participantService = participantService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateParticipantAsync(Guid tournamentId)
    {
        await _participantService.CreateParticipantAsync(tournamentId, this.GetUserId());

        return Ok();
    }
}
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
    private readonly ITeamTournamentService _teamTournamentService;

    public ParticipantController(ITeamTournamentService teamTournamentService)
    {
        _teamTournamentService = teamTournamentService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateParticipantAsync(Guid tournamentId)
    {
        await _teamTournamentService.CreateAsync(tournamentId, this.GetUserId());

        return Ok();
    }
}
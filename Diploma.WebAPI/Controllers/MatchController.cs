using Diploma.WebAPI.BusinessLogic.Interfaces;
using Diploma.WebAPI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.WebAPI.Controllers;

[ApiController]
[Route("api/tournaments/{tournamentId:guid}/matches")]
[Authorize]
public class MatchController : ControllerBase
{
    private readonly IMatchService _matchService;

    public MatchController(IMatchService matchService)
    {
        _matchService = matchService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(Guid tournamentId)
    {
        return Ok(await _matchService.GetAllByTournamentId(tournamentId));
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        return Ok(await _matchService.GetById(id, this.GetUserId()));
    }
}
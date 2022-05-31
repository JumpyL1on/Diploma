using Diploma.Common.Requests;
using Diploma.WebAPI.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.WebAPI.Controllers;

[ApiController]
[Route("api/tournaments")]
[Authorize]
public class TournamentController : ControllerBase
{
    private readonly ITournamentService _tournamentService;

    public TournamentController(ITournamentService tournamentService)
    {
        _tournamentService = tournamentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetByStatus([FromQuery] string status)
    {
        return status switch
        {
            "upcoming" => Ok(await _tournamentService.GetUpcomingTournaments()),
            "current" => Ok(await _tournamentService.GetCurrentTournaments()),
            "finished" => Ok(await _tournamentService.GetFinishedTournaments()),
            _ => BadRequest($"Wrong status {status}")
        };
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return Ok(await _tournamentService.GetById(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateTournamentAsync(CreateTournamentRequest request)
    {
        await _tournamentService.CreateTournamentAsync(request);

        return Ok();
    }
}
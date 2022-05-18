using Diploma.Common.Requests;
using Diploma.WebAPI.BusinessLogic.Interfaces;
using Diploma.WebAPI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.WebAPI.Controllers;

[ApiController]
[Route("api/tournaments")]
//[Authorize]
public class TournamentController : ControllerBase
{
    private readonly ITournamentService _tournamentService;
    
    public TournamentController(ITournamentService tournamentService)
    {
        _tournamentService = tournamentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _tournamentService.GetAll();
        
        return this.FromResult(result);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _tournamentService.GetById(id);

        return this.FromResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTournamentAsync(CreateTournamentRequest request)
    {
        var result = await _tournamentService.CreateTournamentAsync(request);

        return this.FromResult(result);
    }
}
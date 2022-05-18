using Diploma.Common.Requests;
using Diploma.WebAPI.BusinessLogic.Interfaces;
using Diploma.WebAPI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.WebAPI.Controllers;

[ApiController]
[Route("api/teams")]
[Authorize] 
public class TeamController : ControllerBase
{
    public TeamController(ITeamService teamService)
    {
        TeamService = teamService;
    }

    private ITeamService TeamService { get; }
    
    [HttpGet]
    [Route("current")]
    public async Task<IActionResult> GetCurrent()
    {
        var result = await TeamService.GetByUserId(this.GetUserId());
        return this.FromResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateTeamRequest createTeam)
    {
        var result = await TeamService.CreateAsync(createTeam, this.GetUserId());
        return this.FromResult(result);
    }

    [HttpDelete, Route("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await TeamService.DeleteAsync(id, this.GetUserId());
        return this.FromResult(result);
    }
}
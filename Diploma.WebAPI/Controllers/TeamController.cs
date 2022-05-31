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
    private readonly ITeamService _teamService;

    public TeamController(ITeamService teamService)
    {
        _teamService = teamService;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(CreateTeamRequest request)
    {
        await _teamService.CreateAsync(request, this.GetUserId());

        return Ok();
    }

    [HttpDelete, Route("{id:guid}")]
    public async Task<IActionResult> DeleteByIdAsync(Guid id)
    {
        await _teamService.DeleteAsync(id, this.GetUserId());

        return Ok();
    }
}
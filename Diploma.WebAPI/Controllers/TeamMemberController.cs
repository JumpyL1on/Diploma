using Diploma.WebAPI.BusinessLogic.Interfaces;
using Diploma.WebAPI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.WebAPI.Controllers;

[ApiController]
[Route("api/teams/{teamId:guid}/team-members")]
[Authorize]
public class TeamMemberController : ControllerBase
{
    private readonly ITeamMemberService _teamMemberService;

    public TeamMemberController(ITeamMemberService teamMemberService)
    {
        _teamMemberService = teamMemberService;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Guid teamId)
    {
        await _teamMemberService.CreateAsync(teamId, this.GetUserId());

        return Ok();
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> Delete(Guid teamId, Guid id)
    {
        await _teamMemberService.DeleteAsync(id, this.GetUserId());

        return Ok();
    }
}
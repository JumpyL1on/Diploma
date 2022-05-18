﻿using Diploma.WebAPI.BusinessLogic.Interfaces;
using Diploma.WebAPI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.WebAPI.Controllers;

[ApiController]
[Route("api/teams/{teamId:guid}/team-members")]
[Authorize]
public class TeamMemberController : ControllerBase
{
    public TeamMemberController(ITeamMemberService teamMemberService)
    {
        TeamMemberService = teamMemberService;
    }

    private ITeamMemberService TeamMemberService { get; }
    
    [HttpPost]
    public async Task<IActionResult> Post(Guid teamId)
    {
        var result = await TeamMemberService.CreateAsync(this.GetUserId(), teamId);
        return this.FromResult(result);
    }
    
    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> Delete(Guid teamId, Guid id)
    {
        var result = await TeamMemberService.DeleteAsync(id, this.GetUserId());
        return this.FromResult(result);
    }
}
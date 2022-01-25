using System;
using System.Threading.Tasks;
using Backend.Application.Features.CreateTeam;
using Backend.Application.Features.CreateTeamMember;
using Backend.Application.Features.DeleteTeam;
using Backend.Application.Features.DeleteTeamMember;
using Backend.Application.Features.GetCurrentTeam;
using Backend.Core.Entities;
using Backend.WebAPI.Base;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Backend.WebAPI.Controllers;

[Route("api/teams")]
public class TeamsController : AuthorizedApiController
{
    public TeamsController(UserManager<AppUser> userManager, IMediator mediator) : base(userManager, mediator)
    {
    }

    [HttpGet, Route("current")]
    public async Task<IActionResult> GetCurrent()
    {
        var request = new GetCurrentTeamQuery { AppUser = await UserManager.GetUserAsync(User) };
        return Ok(await Mediator.Send(request));
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateTeamCommand request)
    {
        return Ok(await Mediator.Send(request with {AppUser = await UserManager.GetUserAsync(User)}));
    }

    [HttpDelete, Route("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var request = new DeleteTeamCommand { Id = id };
        return Ok(await Mediator.Send(request));
    }

    [HttpPost, Route("{id:guid}/team-members")]
    public async Task<IActionResult> Post(Guid id)
    {
        var request = new CreateTeamMemberCommand
        {
            AppUser = await UserManager.GetUserAsync(User),
            TeamId = id
        };
        return Ok(await Mediator.Send(request));
    }
        
    [HttpDelete, Route("{id:guid}/team-members")]
    public async Task<IActionResult> Delete()
    {
        var request = new DeleteTeamMemberCommand { AppUser = await UserManager.GetUserAsync(User)};
        return Ok(await Mediator.Send(request));
    }
}
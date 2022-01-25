using System;
using System.Threading.Tasks;
using Backend.Application.Features.BeginTournament;
using Backend.Application.Features.CreateTournament;
using Backend.Application.Features.GetAllTournaments;
using Backend.Application.Features.GetCurrentTournament;
using Backend.Application.Features.GetTournamentById;
using Backend.Core.Entities;
using Backend.WebAPI.Base;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Backend.WebAPI.Controllers;

[Route("api/tournaments")]
public class TournamentsController : AuthorizedApiController
{
    public TournamentsController(UserManager<AppUser> userManager, IMediator mediator) : base(userManager, mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await Mediator.Send(new GetAllTournamentsQuery()));
    }

    [HttpGet, Route("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return Ok(await Mediator.Send(new GetTournamentByIdQuery { Id = id }));
    }

    [HttpGet, Route("current")]
    public async Task<IActionResult> GetCurrent()
    {
        var request = new GetCurrentTournamentQuery { AppUser = await UserManager.GetUserAsync(User) };
        return Ok(await Mediator.Send(request));
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateTournamentCommand request)
    {
        return Ok(await Mediator.Send(request));
    }

    [HttpPut, Route("{id:guid}")]
    public async Task<IActionResult> Put(Guid id, BeginTournamentCommand request)
    {
        request.TournamentId = id;
        return Ok(await Mediator.Send(request));
    }
}
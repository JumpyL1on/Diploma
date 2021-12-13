using System;
using System.Threading.Tasks;
using Backend.Application.Features.GetAllTournaments;
using Backend.Application.Features.GetTournamentById;
using Backend.Core.Entities;
using Backend.WebAPI.Base;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Backend.WebAPI.Controllers
{
    [Route("api/tournaments")]
    public class TournamentsController : AuthorizedApiController
    {
        public TournamentsController(UserManager<AppUser> userManager, IMediator mediator) : base(userManager, mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllTournamentsQuery()));
        }

        [HttpGet, Route("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await Mediator.Send(new GetTournamentByIdQuery { Id = id }));
        }
    }
}
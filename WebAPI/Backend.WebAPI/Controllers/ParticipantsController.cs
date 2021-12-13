using System;
using System.Threading.Tasks;
using Backend.Application.Features.JoinTournament;
using Backend.Core.Entities;
using Backend.WebAPI.Base;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Backend.WebAPI.Controllers
{
    [Route("api/tournaments")]
    public class ParticipantsController : AuthorizedApiController
    {
        public ParticipantsController(UserManager<AppUser> userManager, IMediator mediator) : base(userManager,
            mediator)
        {
        }

        [HttpPost, Route("{tournamentId:guid}/participants")]
        public async Task<IActionResult> Create(Guid tournamentId)
        {
            var request = new CreateParticipantCommand
            {
                AppUser = await UserManager.GetUserAsync(User),
                TournamentId = tournamentId
            };
            return Ok(await Mediator.Send(request));
        }
    }
}
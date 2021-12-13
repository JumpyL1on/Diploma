using System;
using System.Threading.Tasks;
using Frontend.Application.Features.GetTournamentById;
using Frontend.Application.Features.JoinTournament;
using Frontend.Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Components;

namespace Frontend.WebAssembly.Pages.Tournaments
{
    public partial class Info
    {
        [Inject] public IMediator Mediator { get; set; }
        [Parameter] public Guid Id { get; set; }
        private TournamentDTO Tournament { get; set; }

        public async Task JoinTournamentAsync()
        {
            var request = new JoinTournamentCommand
            {
                TournamentId = Id
            };
            await Mediator.Send(request);
        }
        
        protected override async Task OnInitializedAsync()
        {
            Tournament = await Mediator.Send(new GetTournamentByIdQuery { Id = Id });
        }
    }
}
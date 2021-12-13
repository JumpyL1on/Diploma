using System;
using System.Threading.Tasks;
using Frontend.Application.Features.GetAllTournaments;
using Frontend.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Components;

namespace Frontend.WebAssembly.Pages.Tournaments
{
    public partial class Index
    {
        [Inject] public IMediator Mediator { get; set; }

        public TournamentPreviewDTO[] Tournaments { get; set; } = Array.Empty<TournamentPreviewDTO>();
        
        protected override async Task OnInitializedAsync()
        {
            Tournaments = await Mediator.Send(new GetAllTournamentsQuery());
        }
    }
}
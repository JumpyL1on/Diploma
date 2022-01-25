using System.Threading.Tasks;
using Frontend.Application.Features.BeginTournament;
using Frontend.Application.Features.GetCurrentTournament;
using Frontend.Domain.Entities;
using Frontend.Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Components;

namespace Frontend.WebAssembly.Pages.CurrentTournamentDetails;

public partial class CurrentTournamentDetails
{
    [Inject] public IMediator Mediator { get; set; }
    public CurrentTournamentDTO CurrentTournament { get; set; }

    protected override async Task OnInitializedAsync()
    {
        CurrentTournament = await Mediator.Send(new GetCurrentTournamentQuery());
    }

    private async Task OnClick()
    {
        var request = new BeginTournamentCommand
        {
            TournamentId = CurrentTournament.Id,
            MaxParticipantsNumber = CurrentTournament.MaxParticipantsNumber
        };
        await Mediator.Send(request);
    }
}
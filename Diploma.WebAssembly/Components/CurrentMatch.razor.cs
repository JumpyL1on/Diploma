using Diploma.Common.DTOs;
using Diploma.WebAssembly.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Diploma.WebAssembly.Components;

public partial class CurrentMatch
{
    [Inject] public NavigationManager NavigationManager { get; set; }
    [Inject] public IMatchService MatchService { get; set; }
    private MatchDTO? match;

    public void BackToTournament()
    {
        NavigationManager.NavigateTo($"/tournaments/{match.TournamentId}");
    }

    protected override async Task OnInitializedAsync()
    {
        match = await MatchService.GetCurrentMatch();
    }
}
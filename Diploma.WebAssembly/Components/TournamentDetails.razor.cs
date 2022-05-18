using Diploma.Common.DTOs;
using Diploma.WebAssembly.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Diploma.WebAssembly.Components;

public partial class TournamentDetails
{
    [Parameter] public Guid Id { get; set; }
    [Inject] public ITournamentService TournamentService { get; set; }
    private TournamentDetailsDTO tournamentDetails;

    private async Task JoinTournamentAsync()
    {
        
    }
    
    protected override async Task OnInitializedAsync()
    {
        tournamentDetails = await TournamentService.GetById(Id);
    }
}
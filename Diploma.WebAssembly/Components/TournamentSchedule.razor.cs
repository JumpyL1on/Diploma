using Diploma.Common.DTOs;
using Diploma.WebAssembly.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Diploma.WebAssembly.Components;

public partial class TournamentSchedule
{
    [Parameter] public Guid TournamentId { get; set; }
    [Inject] private IMatchService MatchService { get; set; } = null!;
    private List<MatchDTO>? _matches;

    protected override async Task OnInitializedAsync()
    {
        _matches = await MatchService.GetAllByTournamentId(TournamentId);
    }
}
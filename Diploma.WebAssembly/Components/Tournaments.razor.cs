using Diploma.Common.DTOs;
using Diploma.WebAssembly.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Diploma.WebAssembly.Components;

public partial class Tournaments
{
    [Parameter] public string Status { get; set; }
    [Inject] public ITournamentService TournamentService { get; set; }
    private List<TournamentDTO>? _tournaments;

    protected override async Task OnInitializedAsync()
    {
        _tournaments = await TournamentService.GetAllByStatus(Status);
    }
}
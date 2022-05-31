using Diploma.Common.DTOs;
using Diploma.WebAssembly.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Diploma.WebAssembly.Components;

public partial class Tournaments
{
    private List<TournamentDTO>? _tournaments;
    [Inject] public ITournamentService TournamentService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        _tournaments = await TournamentService.GetAll();
    }
}
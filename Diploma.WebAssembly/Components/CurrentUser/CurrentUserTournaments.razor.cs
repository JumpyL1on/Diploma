using Diploma.Common.DTOs;
using Diploma.WebAssembly.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Diploma.WebAssembly.Components.CurrentUser;

public partial class CurrentUserTournaments
{
    [Inject] public ICurrentUserService CurrentUserService { get; set; } = null!;
    private List<TournamentDTO>? _tournaments;

    protected override async Task OnInitializedAsync()
    {
        _tournaments = await CurrentUserService.GetAllTournamentsAsync();
    }
}
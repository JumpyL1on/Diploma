using Diploma.Common.DTOs;
using Diploma.WebAssembly.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Diploma.WebAssembly.Components.CurrentUser;

public partial class CurrentUserGames
{
    [Inject] public ICurrentUserService CurrentUserService { get; set; } = null!;
    private List<UserGameDTO>? _games;

    protected override async Task OnInitializedAsync()
    {
        _games = await CurrentUserService.GetAllGamesAsync();
    }
}
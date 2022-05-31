﻿using Diploma.Common.DTOs;
using Diploma.WebAssembly.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Diploma.WebAssembly.Components;

public partial class UserGames
{
    [Inject] public ICurrentUserService CurrentUserService { get; set; } = null!;
    private List<GameDTO>? _games;

    protected override async Task OnInitializedAsync()
    {
        await Task.Delay(1000);
        _games = await CurrentUserService.GetAllGamesAsync();
    }
}
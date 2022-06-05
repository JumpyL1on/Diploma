﻿using Diploma.Common.DTOs;
using Diploma.WebAssembly.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Diploma.WebAssembly.Components;

public partial class UserTournaments
{
    [Inject] public ICurrentUserService CurrentUserService { get; set; } = null!;
    private List<TournamentDTO>? _tournaments;

    protected override async Task OnInitializedAsync()
    {
        _tournaments = await CurrentUserService.GetAllTournamentsAsync();
    }
}
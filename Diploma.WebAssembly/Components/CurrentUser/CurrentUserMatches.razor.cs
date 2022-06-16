using Diploma.Common.DTOs;
using Diploma.WebAssembly.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Diploma.WebAssembly.Components.CurrentUser;

public partial class CurrentUserMatches
{
    [Inject] private ICurrentUserService CurrentUserService { get; set; } = null!;
    private List<MatchDTO>? _matches;

    protected override async Task OnInitializedAsync()
    {
        _matches = await CurrentUserService.GetAllMatchesAsync();
    }
}
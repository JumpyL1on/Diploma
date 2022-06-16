using Diploma.Common.DTOs;
using Diploma.WebAssembly.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Diploma.WebAssembly.Components;

public partial class MatchDetails
{
    [Parameter] public Guid Id { get; set; }
    [Inject] public IMatchService MatchService { get; set; } = null!;
    [Inject] public ICurrentUserService CurrentUserService { get; set; } = null!;
    private MatchDetailsDTO? _match;

    private async Task OnClickAsync()
    {
        await CurrentUserService.InviteToLobbyAsync(Id);
    }
    
    protected override async Task OnInitializedAsync()
    {
        _match = await MatchService.GetByIdAsync(Id);
    }
}
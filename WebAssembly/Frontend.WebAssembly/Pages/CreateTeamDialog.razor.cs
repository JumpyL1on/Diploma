using System.Threading.Tasks;
using Frontend.Application.Features.CreateTeam;
using Frontend.Application.Features.RefreshJWT;
using Frontend.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

namespace Frontend.WebAssembly.Pages;

public partial class CreateTeamDialog
{
    [Inject] public IMediator Mediator { get; set; }
    [Inject] public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
    [CascadingParameter] public MudDialogInstance MudDialog { get; set; }

    private CreateTeamCommand Request { get; } = new();

    private async Task OnValidSubmitAsync()
    {
        await Mediator.Send(Request);
        await Mediator.Send(new RefreshJWTCommand());
        await ((AppAuthenticationStateProvider)AuthenticationStateProvider).Notify();
        MudDialog.Close(DialogResult.Ok(true));
    }
}
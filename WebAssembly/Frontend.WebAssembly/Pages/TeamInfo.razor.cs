using System;
using System.Threading.Tasks;
using Frontend.Application.Features.DeleteTeam;
using Frontend.Application.Features.DeleteTeamMember;
using Frontend.Application.Features.GetTeamById;
using Frontend.Domain.Entities;
using Frontend.Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Frontend.WebAssembly.Pages;

public partial class TeamInfo
{
    [Inject] public IMediator Mediator { get; set; }

    [Inject] private NavigationManager NavigationManager { get; set; }

    private TeamDTO Team { get; set; }

    [Inject] public IDialogService DialogService { get; set; }

    private async Task OnClickAsync()
    {
        var result = await DialogService.ShowMessageBox(
            "Удалить команду",
            "Данное действие невозможно отменить",
            "Удалить",
            "Отменить");
        if (result == true)
        {
            await Mediator.Send(new DeleteTeamCommand { Id = Team.Id });
            Team = null;
        }
    }

    private async Task OpenDialog()
    {
        var dialog = DialogService.Show<CreateTeamDialog>("Создать команду");
        var result = await dialog.Result;
        if (!result.Cancelled)
            Team = await Mediator.Send(new GetCurrentTeamQuery());
    }

    private async Task OnLeaveAsync()
    {
        var result = await DialogService.ShowMessageBox(
            "Покинуть команду",
            "Данное действие невозможно отменить",
            "Подтвердить",
            "Отменить");
        if (result == true)
        {
            await Mediator.Send(new DeleteTeamMemberCommand { Id = Team.Id });
            Team = null;
        }
    }

    protected override async Task OnInitializedAsync()
    {
        Team = await Mediator.Send(new GetCurrentTeamQuery());
    }
}
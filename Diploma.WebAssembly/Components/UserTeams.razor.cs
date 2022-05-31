﻿using Diploma.Common.DTOs;
using Diploma.WebAssembly.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Diploma.WebAssembly.Components;

public partial class UserTeams
{
    [Inject] public ICurrentUserService CurrentUserService { get; set; }
    [Inject] public IDialogService DialogService { get; set; }
    [Inject] public NavigationManager NavManager { get; set; }
    private List<TeamDTO>? _teams;

    private async Task OnClickAsync()
    {
        var dialog = DialogService.Show<CreateTeamDialog>("Создание организации");

        var result = await dialog.Result;

        if (!result.Cancelled)
        {
            _teams = await CurrentUserService.GetAllTeamsAsync();
        }
    }

    /*private async Task OnClickAsync()
    {
        var result = await DialogService.ShowMessageBox(
            "Удалить команду",
            "Данное действие невозможно отменить",
            "Удалить",
            "Отменить");
        
        if (result == true)
        {
            //await TeamService.DeleteAsync(_teams!.Id);
            _teams = null;
        }
    }*/

    private async Task OnLeaveAsync()
    {
        var result = await DialogService.ShowMessageBox(
            "Покинуть команду",
            "Данное действие невозможно отменить",
            "Подтвердить",
            "Отменить");
        if (result == true)
        {
            //await TeamService.Send(new DeleteTeamMemberCommand { Id = Team.Id });
            //Team = null;
        }
    }

    protected override async Task OnInitializedAsync()
    {
        await Task.Delay(1000);
        _teams = await CurrentUserService.GetAllTeamsAsync();
    }
}
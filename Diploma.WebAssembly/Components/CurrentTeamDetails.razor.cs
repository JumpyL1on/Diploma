using Diploma.Common.DTOs;
using Diploma.WebAssembly.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Diploma.WebAssembly.Components;

public partial class CurrentTeamDetails
{
    [Inject] private ITeamService TeamService { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }
    [Inject] public IDialogService DialogService { get; set; }
    private TeamDTO? Team { get; set; }

    private async Task OnClickAsync()
    {
        var result = await DialogService.ShowMessageBox(
            "Удалить команду",
            "Данное действие невозможно отменить",
            "Удалить",
            "Отменить");
        
        if (result == true)
        {
            await TeamService.DeleteAsync(Team!.Id);
            Team = null;
        }
    }

    private async Task OpenDialog()
    {
        var dialog = DialogService.Show<CreateTeamDialog>("Создать команду");
        
        var result = await dialog.Result;

        if (!result.Cancelled)
        {
            Team = await TeamService.GetCurrentAsync();
        }
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
            //await TeamService.Send(new DeleteTeamMemberCommand { Id = Team.Id });
            //Team = null;
        }
    }

    protected override async Task OnInitializedAsync()
    {
        Team = await TeamService.GetCurrentAsync();
    }
}
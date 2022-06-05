using Diploma.Common.DTOs;
using Diploma.WebAssembly.BusinessLogic.Interfaces;
using Diploma.WebAssembly.Components.Dialogs;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Diploma.WebAssembly.Components;

public partial class UserOrganizations
{
    [Inject] public ICurrentUserService CurrentUserService { get; set; }
    [Inject] public IDialogService DialogService { get; set; }
    [Inject] public NavigationManager NavManager { get; set; }
    private List<OrganizationDTO>? _organizations;

    private async Task OnClickAsync()
    {
        var dialog = DialogService.Show<CreateOrganizationDialog>("Создание организации");

        var result = await dialog.Result;

        if (!result.Cancelled)
        {
            _organizations = await CurrentUserService.GetAllOrganizationsAsync();
        }
    }
    
    protected override async Task OnInitializedAsync()
    {
        _organizations = await CurrentUserService.GetAllOrganizationsAsync();
    }
}
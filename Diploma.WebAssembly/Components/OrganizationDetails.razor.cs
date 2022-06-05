using Diploma.Common.DTOs;
using Diploma.WebAssembly.BusinessLogic.Interfaces;
using Diploma.WebAssembly.Components.Dialogs;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Diploma.WebAssembly.Components;

public partial class OrganizationDetails
{
    [Parameter] public Guid Id { get; set; }
    [Inject] public IOrganizationService OrganizationService { get; set; } = null!;
    [Inject] public IDialogService DialogService { get; set; } = null!;
    [Inject] public ITournamentService TournamentService { get; set; } = null!;
    private OrganizationDetailsDTO? _organization;
    private List<(OrganizationMemberDTO, int)> _organizationMembers = null!;

    private async Task OnClickAsync()
    {
        var dialog = DialogService.Show<CreateTournamentDialog>("Создание турнира");

        await dialog.Result;
    }

    protected override async Task OnInitializedAsync()
    {
        _organization = await OrganizationService.GetByIdAsync(Id);

        _organizationMembers = _organization.OrganizationMembers
            .Zip(Enumerable.Range(1, _organization.OrganizationMembers.Count))
            .ToList();
    }
}
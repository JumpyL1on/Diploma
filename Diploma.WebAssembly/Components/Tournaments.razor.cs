using Diploma.Common.DTOs;
using Diploma.WebAssembly.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Diploma.WebAssembly.Components;

public partial class Tournaments
{
    private List<TournamentDTO>? _tournaments;
    [Inject] public ITournamentService TournamentService { get; set; }
    [Inject] public NavigationManager NavigationManager { get; set; }

    public void OnRowClick(TableRowClickEventArgs<TournamentDTO> tableRowClickEventArgs)
    {
        NavigationManager.NavigateTo($"/{tableRowClickEventArgs.Item.Id}");
    }

    protected override async Task OnInitializedAsync()
    {
        _tournaments = await TournamentService.GetAll();
    }
}